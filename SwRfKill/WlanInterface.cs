using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Threading;

namespace Net
{
    /// <summary>
    /// Represents a Wifi network interface.
    /// </summary>
    public class WlanInterface : IWlanInterface
    {
        private readonly WlanClient m_Client;
        private Wlan.WlanInterfaceInfo m_Info;

        /// <summary>
        /// Represents a method that will handle <see cref="WlanNotification"/> events.
        /// </summary>
        /// <param name="notifyData">The notification data.</param>
        public delegate void WlanNotificationEventHandler(Wlan.WlanNotificationData notifyData);

        /// <summary>
        /// Represents a method that will handle <see cref="WlanConnectionNotification"/> events.
        /// </summary>
        /// <param name="notifyData">The notification data.</param>
        /// <param name="connNotifyData">The notification data.</param>
        public delegate void WlanConnectionNotificationEventHandler(Wlan.WlanNotificationData notifyData, Wlan.WlanConnectionNotificationData connNotifyData);

        /// <summary>
        /// Represents a method that will handle <see cref="WlanMsmNotification"/> events.
        /// </summary>
        /// <param name="notifyData">The notification data.</param>
        /// <param name="msmNotifyData">The notification data.</param>
        public delegate void WlanMsmNotificationEventHandler(Wlan.WlanNotificationData notifyData, Wlan.WlanMsmNotificationData msmNotifyData);

        /// <summary>
        /// Represents a method that will handle <see cref="WlanReasonNotification"/> events.
        /// </summary>
        /// <param name="notifyData">The notification data.</param>
        /// <param name="reasonCode">The reason code.</param>
        public delegate void WlanReasonNotificationEventHandler(Wlan.WlanNotificationData notifyData, Wlan.WlanReasonCode reasonCode);

        /// <summary>
        /// Represents a method that will handle <see cref="WlanRadioStateChangedNotification"/> events.
        /// </summary>
        /// <param name="notifyData">The notification data.</param>
        /// <param name="radioState">The radio state.</param>
        public delegate void WlanRadioStateChangedEventHandler(Wlan.WlanNotificationData notifyData, Wlan.WlanPhyRadioState radioState);

        /// <summary>
        /// Represents a method that will handle <see cref="WlanRadioStateChangedNotification"/> events.
        /// </summary>
        /// <param name="notifyData">The notification data.</param>
        /// <param name="signalQuality">The signal quality.</param>
        public delegate void WlanSignalQualityChangedEventHandler(Wlan.WlanNotificationData notifyData, Wlan.WlanSignalQuality signalQuality);

        /// <summary>
        /// Represents a method that will handle <see cref="WlanRadioStateChangedNotification"/> events.
        /// </summary>
        /// <param name="notifyData">The notification data.</param>
        /// <param name="powerSetting">The power setting.</param>
        public delegate void WlanPowerSettingChangedEventHandler(Wlan.WlanNotificationData notifyData, Wlan.WlanPowerSetting powerSetting);

        /// <summary>
        /// Occurs when an event of any kind occurs on a WLAN interface.
        /// </summary>
        public event WlanNotificationEventHandler WlanNotification;

        /// <summary>
        /// Occurs when a WLAN interface changes connection state.
        /// </summary>
        public event WlanConnectionNotificationEventHandler WlanConnectionNotification;

        /// <summary>
        /// Occurs when a WLAN interface changes MSM state.
        /// </summary>
        public event WlanMsmNotificationEventHandler WlanMsmNotification;

        /// <summary>
        /// Occurs when a WLAN operation fails due to some reason.
        /// </summary>
        public event WlanReasonNotificationEventHandler WlanReasonNotification;

        /// <summary>
        /// Occurs when the radio state changed.
        /// </summary>
        public event WlanRadioStateChangedEventHandler WlanRadioStateChangedNotification;

        /// <summary>
        /// Occurs when the signal quality changed.
        /// </summary>
        public event WlanSignalQualityChangedEventHandler WlanSignalQualityChangedNotification;

        /// <summary>
        /// Occurs when the signal quality changed.
        /// </summary>
        public event WlanPowerSettingChangedEventHandler WlanPowerSettingChangedNotification;

        /// <summary>
        /// Occurs when scan is completed successfully.
        /// </summary>
        public event WlanNotificationEventHandler WlanScanCompleteNotification;

        /// <summary>
        /// Occurs when scan is completed successfully.
        /// </summary>
        public event WlanReasonNotificationEventHandler WlanScanFailNotification;

        private bool m_QueueEvents;
        private readonly AutoResetEvent m_EventQueueFilled = new AutoResetEvent(false);
        private readonly Queue<object> m_EventQueue = new Queue<object>();

        private struct WlanConnectionNotificationEventData
        {
            public Wlan.WlanNotificationData NotifyData;
            public Wlan.WlanConnectionNotificationData ConnNotifyData;
        }


        private struct WlanReasonNotificationData
        {
#pragma warning disable 414
            public Wlan.WlanNotificationData NotifyData;
            public Wlan.WlanReasonCode ReasonCode;
#pragma warning restore 414
        }

        internal WlanInterface(WlanClient client, Wlan.WlanInterfaceInfo info)
        {
            m_Client = client;
            m_Info = info;
        }

        /// <summary>
        /// Sets a parameter of the interface whose data type is <see cref="int"/>.
        /// </summary>
        /// <param name="opCode">The opcode of the parameter.</param>
        /// <param name="value">The value to set.</param>
        private void SetInterfaceInt(Wlan.WlanIntfOpcode opCode, int value)
        {
            IntPtr valuePtr = Marshal.AllocHGlobal(sizeof(int));
            Marshal.WriteInt32(valuePtr, value);
            try
            {
                Wlan.ThrowIfError(
                    Wlan.WlanSetInterface(m_Client.ClientHandle, m_Info.interfaceGuid, opCode, sizeof(int), valuePtr, IntPtr.Zero));
            }
            finally
            {
                Marshal.FreeHGlobal(valuePtr);
            }
        }

        private void SetInterface<T>(Wlan.WlanIntfOpcode opCode, T value) where T : struct
        {
            Type outputType = typeof(T).IsEnum ? Enum.GetUnderlyingType(typeof(T)) : typeof(T);
            int valueSize = Marshal.SizeOf(outputType);
            IntPtr valuePtr = Marshal.AllocHGlobal(valueSize);

            try
            {
                Marshal.StructureToPtr(value, valuePtr, true);

                Wlan.ThrowIfError(
                    Wlan.WlanSetInterface(m_Client.ClientHandle, m_Info.interfaceGuid, opCode, (uint)valueSize, valuePtr, IntPtr.Zero));
            }
            finally
            {
                Marshal.FreeHGlobal(valuePtr);
            }
        }

        /// <summary>
        /// Gets a parameter of the interface whose data type is <see cref="int"/>.
        /// </summary>
        /// <param name="opCode">The opcode of the parameter.</param>
        /// <returns>The integer value.</returns>
        private int GetInterfaceInt(Wlan.WlanIntfOpcode opCode)
        {
            IntPtr valuePtr;
            int valueSize;
            Wlan.WlanOpcodeValueType opcodeValueType;
            Wlan.ThrowIfError(
                Wlan.WlanQueryInterface(m_Client.ClientHandle, m_Info.interfaceGuid, opCode, IntPtr.Zero, out valueSize, out valuePtr, out opcodeValueType));
            try
            {
                return Marshal.ReadInt32(valuePtr);
            }
            finally
            {
                Wlan.WlanFreeMemory(valuePtr);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WlanInterface"/> is automatically configured.
        /// </summary>
        /// <value><c>true</c> if "autoconf" is enabled; otherwise, <c>false</c>.</value>
        public bool Autoconf
        {
            get
            {
                return GetInterfaceInt(Wlan.WlanIntfOpcode.AutoconfEnabled) != 0;
            }
            set
            {
                SetInterfaceInt(Wlan.WlanIntfOpcode.AutoconfEnabled, value ? 1 : 0);
            }
        }

        /// <summary>
        /// Gets or sets the BSS type for the indicated interface.
        /// </summary>
        /// <value>The type of the BSS.</value>
        public Wlan.Dot11BssType BssType
        {
            get
            {
                return (Wlan.Dot11BssType)GetInterfaceInt(Wlan.WlanIntfOpcode.BssType);
            }
            set
            {
                SetInterfaceInt(Wlan.WlanIntfOpcode.BssType, (int)value);
            }
        }

        /// <summary>
        /// Gets the state of the interface.
        /// </summary>
        /// <value>The state of the interface.</value>
        public Wlan.WlanInterfaceState InterfaceState => (Wlan.WlanInterfaceState)GetInterfaceInt(Wlan.WlanIntfOpcode.InterfaceState);

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>The channel.</value>
        /// <remarks>Not supported on Windows XP SP2.</remarks>
        public int Channel => GetInterfaceInt(Wlan.WlanIntfOpcode.ChannelNumber);

        /// <summary>
        /// Gets the RSSI.
        /// </summary>
        /// <value>The RSSI.</value>
        /// <remarks>Not supported on Windows XP SP2.</remarks>
        public int Rssi => GetInterfaceInt(Wlan.WlanIntfOpcode.RSSI);

        public Wlan.WlanInterfaceCapability InterfaceCapability
        {
            get
            {
                int size = Marshal.SizeOf(typeof(Wlan.WlanInterfaceCapability));
                IntPtr valuePtr = Marshal.AllocHGlobal(size);

                try
                {
                    Wlan.ThrowIfError(Wlan.WlanGetInterfaceCapability(m_Client.ClientHandle, m_Info.interfaceGuid,
                        IntPtr.Zero, out valuePtr));

                    return (Wlan.WlanInterfaceCapability)Marshal.PtrToStructure(valuePtr, typeof(Wlan.WlanInterfaceCapability));
                }
                finally
                {
                    Wlan.WlanFreeMemory(valuePtr);
                }
            }
        }

        /// <summary>
        /// Gets the radio state.
        /// </summary>
        /// <value>The radio state.</value>
        /// <remarks>Not supported on Windows XP.</remarks>
        public Wlan.WlanRadioState RadioState
        {
            get
            {
                int valueSize;
                IntPtr valuePtr;
                Wlan.WlanOpcodeValueType opcodeValueType;
                Wlan.ThrowIfError(
                    Wlan.WlanQueryInterface(m_Client.ClientHandle, m_Info.interfaceGuid, Wlan.WlanIntfOpcode.RadioState, IntPtr.Zero, out valueSize, out valuePtr, out opcodeValueType));
                try
                {
                    return (Wlan.WlanRadioState)Marshal.PtrToStructure(valuePtr, typeof(Wlan.WlanRadioState));
                }
                finally
                {
                    Wlan.WlanFreeMemory(valuePtr);
                }
            }
        }

        /// <summary>
        /// Gets the current operation mode.
        /// </summary>
        /// <value>The current operation mode.</value>
        /// <remarks>Not supported on Windows XP SP2.</remarks>
        public Wlan.Dot11OperationMode CurrentOperationMode => (Wlan.Dot11OperationMode)GetInterfaceInt(Wlan.WlanIntfOpcode.CurrentOperationMode);

        /// <summary>
        /// Gets the attributes of the current connection.
        /// </summary>
        /// <value>The current connection attributes.</value>
        /// <exception cref="Win32Exception">An exception with code 0x0000139F (The group or resource is not in the correct state to perform the requested operation.) will be thrown if the interface is not connected to a network.</exception>
        public Wlan.WlanConnectionAttributes CurrentConnection
        {
            get
            {
                int valueSize;
                IntPtr valuePtr;
                Wlan.WlanOpcodeValueType opcodeValueType;
                Wlan.ThrowIfError(
                    Wlan.WlanQueryInterface(m_Client.ClientHandle, m_Info.interfaceGuid, Wlan.WlanIntfOpcode.CurrentConnection, IntPtr.Zero, out valueSize, out valuePtr, out opcodeValueType));
                try
                {
                    return (Wlan.WlanConnectionAttributes)Marshal.PtrToStructure(valuePtr, typeof(Wlan.WlanConnectionAttributes));
                }
                finally
                {
                    Wlan.WlanFreeMemory(valuePtr);
                }
            }
        }

        public void SetPhyRadioState(Wlan.WlanPhyRadioState phyRadioState)
        {
            SetInterface(Wlan.WlanIntfOpcode.RadioState, phyRadioState);
        }

        /// <summary>
        /// Requests a scan for available networks.
        /// </summary>
        /// <remarks>
        /// The method returns immediately. Progress is reported through the <see cref="WlanNotification"/> event.
        /// </remarks>
        public void Scan()
        {
            Wlan.ThrowIfError(
                Wlan.WlanScan(m_Client.ClientHandle, m_Info.interfaceGuid, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero));
        }

        /// <summary>
        /// Converts a pointer to a available networks list (header + entries) to an array of available network entries.
        /// </summary>
        /// <param name="availNetListPtr">A pointer to an available networks list's header.</param>
        /// <returns>An array of available network entries.</returns>
        private static Wlan.WlanAvailableNetwork[] ConvertAvailableNetworkListPtr(IntPtr availNetListPtr)
        {
            Wlan.WlanAvailableNetworkListHeader availNetListHeader = (Wlan.WlanAvailableNetworkListHeader)Marshal.PtrToStructure(availNetListPtr, typeof(Wlan.WlanAvailableNetworkListHeader));
            long availNetListIt = availNetListPtr.ToInt64() + Marshal.SizeOf(typeof(Wlan.WlanAvailableNetworkListHeader));
            Wlan.WlanAvailableNetwork[] availNets = new Wlan.WlanAvailableNetwork[availNetListHeader.numberOfItems];
            for (int i = 0; i < availNetListHeader.numberOfItems; ++i)
            {
                availNets[i] = (Wlan.WlanAvailableNetwork)Marshal.PtrToStructure(new IntPtr(availNetListIt), typeof(Wlan.WlanAvailableNetwork));
                availNetListIt += Marshal.SizeOf(typeof(Wlan.WlanAvailableNetwork));
            }
            return availNets;
        }

        /// <summary>
        /// Retrieves the list of available networks.
        /// </summary>
        /// <param name="flags">Controls the type of networks returned.</param>
        /// <returns>A list of the available networks.</returns>
        public Wlan.WlanAvailableNetwork[] GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags flags)
        {
            IntPtr availNetListPtr;
            Wlan.ThrowIfError(
                Wlan.WlanGetAvailableNetworkList(m_Client.ClientHandle, m_Info.interfaceGuid, flags, IntPtr.Zero, out availNetListPtr));
            try
            {
                return ConvertAvailableNetworkListPtr(availNetListPtr);
            }
            finally
            {
                Wlan.WlanFreeMemory(availNetListPtr);
            }
        }

        /// <summary>
        /// Converts a pointer to a BSS list (header + entries) to an array of BSS entries.
        /// </summary>
        /// <param name="bssListPtr">A pointer to a BSS list's header.</param>
        /// <returns>An array of BSS entries.</returns>
        private static Wlan.WlanBssEntry[] ConvertBssListPtr(IntPtr bssListPtr)
        {
            Wlan.WlanBssListHeader bssListHeader = (Wlan.WlanBssListHeader)Marshal.PtrToStructure(bssListPtr, typeof(Wlan.WlanBssListHeader));
            long bssListIt = bssListPtr.ToInt64() + Marshal.SizeOf(typeof(Wlan.WlanBssListHeader));
            Wlan.WlanBssEntry[] bssEntries = new Wlan.WlanBssEntry[bssListHeader.numberOfItems];
            for (int i = 0; i < bssListHeader.numberOfItems; ++i)
            {
                bssEntries[i] = (Wlan.WlanBssEntry)Marshal.PtrToStructure(new IntPtr(bssListIt), typeof(Wlan.WlanBssEntry));
                bssListIt += Marshal.SizeOf(typeof(Wlan.WlanBssEntry));
            }
            return bssEntries;
        }

        /// <summary>
        /// Retrieves the basic service sets (BSS) list of all available networks.
        /// </summary>
        public Wlan.WlanBssEntry[] GetNetworkBssList()
        {
            IntPtr bssListPtr;
            Wlan.ThrowIfError(
                Wlan.WlanGetNetworkBssList(m_Client.ClientHandle, m_Info.interfaceGuid, IntPtr.Zero, Wlan.Dot11BssType.Any, false, IntPtr.Zero, out bssListPtr));
            try
            {
                return ConvertBssListPtr(bssListPtr);
            }
            finally
            {
                Wlan.WlanFreeMemory(bssListPtr);
            }
        }

        /// <summary>
        /// Retrieves the basic service sets (BSS) list of the specified network.
        /// </summary>
        /// <param name="ssid">Specifies the SSID of the network from which the BSS list is requested.</param>
        /// <param name="bssType">Indicates the BSS type of the network.</param>
        /// <param name="securityEnabled">Indicates whether security is enabled on the network.</param>
        /// <returns></returns>
        public Wlan.WlanBssEntry[] GetNetworkBssList(Wlan.Dot11Ssid ssid, Wlan.Dot11BssType bssType, bool securityEnabled)
        {
            IntPtr ssidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ssid));
            Marshal.StructureToPtr(ssid, ssidPtr, false);
            try
            {
                IntPtr bssListPtr;
                Wlan.ThrowIfError(
                    Wlan.WlanGetNetworkBssList(m_Client.ClientHandle, m_Info.interfaceGuid, ssidPtr, bssType, securityEnabled, IntPtr.Zero, out bssListPtr));
                try
                {
                    return ConvertBssListPtr(bssListPtr);
                }
                finally
                {
                    Wlan.WlanFreeMemory(bssListPtr);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ssidPtr);
            }
        }

        /// <summary>
        /// Disconnects from the network.
        /// </summary>
        public void Disconnect()
        {
            Wlan.ThrowIfError(
                Wlan.WlanDisconnect(m_Client.ClientHandle, m_Info.interfaceGuid, IntPtr.Zero));
        }

        /// <summary>
        /// Connects to a network defined by a connection parameters structure.
        /// </summary>
        /// <param name="connectionParams">The connection paramters.</param>
        private void Connect(Wlan.WlanConnectionParameters connectionParams)
        {
            Trace.WriteLine($"Connect with the following parameters: {connectionParams}, " +
               $"ClientHandle: {m_Client.ClientHandle}, Interface GUID: {m_Info.interfaceGuid}");

            Wlan.ThrowIfError(
                Wlan.WlanConnect(m_Client.ClientHandle, m_Info.interfaceGuid, ref connectionParams, IntPtr.Zero));
        }

        /// <summary>
        /// Requests a connection (association) to the specified wireless network.
        /// </summary>
        /// <remarks>
        /// The method returns immediately. Progress is reported through the <see cref="WlanNotification"/> event.
        /// </remarks>
        public void Connect(Wlan.WlanConnectionMode connectionMode, Wlan.Dot11BssType bssType, string profile)
        {
            Wlan.WlanConnectionParameters connectionParams = new Wlan.WlanConnectionParameters
            {
                wlanConnectionMode = connectionMode,
                profile = profile,
                dot11BssType = bssType,
                flags = 0
            };
            Connect(connectionParams);
        }

        /// <summary>
        /// Connects (associates) to the specified wireless network, returning either on a success to connect
        /// or a failure.
        /// </summary>
        /// <param name="connectionMode"></param>
        /// <param name="bssType"></param>
        /// <param name="profile"></param>
        /// <param name="connectTimeout"></param>
        /// <returns></returns>
        public bool ConnectSynchronously(Wlan.WlanConnectionMode connectionMode, Wlan.Dot11BssType bssType, string profile, int connectTimeout)
        {
            m_QueueEvents = true;
            try
            {
                Connect(connectionMode, bssType, profile);
                while (m_QueueEvents && m_EventQueueFilled.WaitOne(connectTimeout, true))
                {
                    lock (m_EventQueue)
                    {
                        while (m_EventQueue.Count != 0)
                        {
                            object e = m_EventQueue.Dequeue();
                            if (e is WlanConnectionNotificationEventData)
                            {
                                WlanConnectionNotificationEventData wlanConnectionData = (WlanConnectionNotificationEventData)e;
                                // Check if the conditions are good to indicate either success or failure.
                                if (wlanConnectionData.NotifyData.notificationSource == Wlan.WlanNotificationSource.ACM)
                                {
                                    switch ((Wlan.WlanNotificationCodeAcm)wlanConnectionData.NotifyData.notificationCode)
                                    {
                                        case Wlan.WlanNotificationCodeAcm.ConnectionComplete:
                                            if (wlanConnectionData.ConnNotifyData.profileName == profile)
                                                return true;
                                            break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                m_QueueEvents = false;
                lock (m_EventQueue)
                {
                    m_EventQueue.Clear();
                }
            }
            return false; // timeout expired and no "connection complete"
        }

        /// <summary>
        /// Connects to the specified wireless network.
        /// </summary>
        /// <remarks>
        /// The method returns immediately. Progress is reported through the <see cref="WlanNotification"/> event.
        /// </remarks>
        public void Connect(Wlan.WlanConnectionMode connectionMode, Wlan.Dot11BssType bssType, Wlan.Dot11Ssid ssid, Wlan.WlanConnectionFlags flags)
        {
            Wlan.WlanConnectionParameters connectionParams = new Wlan.WlanConnectionParameters
            {
                wlanConnectionMode = connectionMode,
                dot11SsidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ssid))
            };
            Marshal.StructureToPtr(ssid, connectionParams.dot11SsidPtr, false);
            connectionParams.dot11BssType = bssType;
            connectionParams.flags = flags;
            Connect(connectionParams);
            Marshal.DestroyStructure(connectionParams.dot11SsidPtr, ssid.GetType());
            Marshal.FreeHGlobal(connectionParams.dot11SsidPtr);
        }

        /// <summary>
        /// Deletes a profile.
        /// </summary>
        /// <param name="profileName">
        /// The name of the profile to be deleted. Profile names are case-sensitive.
        /// On Windows XP SP2, the supplied name must match the profile name derived automatically from the SSID of the network. For an infrastructure network profile, the SSID must be supplied for the profile name. For an ad hoc network profile, the supplied name must be the SSID of the ad hoc network followed by <c>-adhoc</c>.
        /// </param>
        public void DeleteProfile(string profileName)
        {
            Wlan.ThrowIfError(
                Wlan.WlanDeleteProfile(m_Client.ClientHandle, m_Info.interfaceGuid, profileName, IntPtr.Zero));
        }

        /// <summary>
        /// Sets the profile.
        /// </summary>
        /// <param name="flags">The flags to set on the profile.</param>
        /// <param name="profileXml">The XML representation of the profile. On Windows XP SP 2, special care should be taken to adhere to its limitations.</param>
        /// <param name="overwrite">If a profile by the given name already exists, then specifies whether to overwrite it (if <c>true</c>) or return an error (if <c>false</c>).</param>
        /// <returns>The resulting code indicating a success or the reason why the profile wasn't valid.</returns>
        public Wlan.WlanReasonCode SetProfile(Wlan.WlanProfileFlags flags, string profileXml, bool overwrite)
        {
            Wlan.WlanReasonCode reasonCode;
            Wlan.ThrowIfError(
                Wlan.WlanSetProfile(m_Client.ClientHandle, m_Info.interfaceGuid, flags, profileXml, null, overwrite, IntPtr.Zero, out reasonCode));
            return reasonCode;
        }

        /// <summary>
        /// Gets the profile's XML specification.
        /// </summary>
        /// <param name="profileName">The name of the profile.</param>
        /// <returns>The XML document.</returns>
        public string GetProfileXml(string profileName)
        {
            IntPtr profileXmlPtr;
            Wlan.WlanProfileFlags flags;
            Wlan.WlanAccess access;
            Wlan.ThrowIfError(
                Wlan.WlanGetProfile(m_Client.ClientHandle, m_Info.interfaceGuid, profileName, IntPtr.Zero, out profileXmlPtr, out flags,
                    out access));
            try
            {
                return Marshal.PtrToStringUni(profileXmlPtr);
            }
            finally
            {
                Wlan.WlanFreeMemory(profileXmlPtr);
            }
        }

        /// <summary>
        /// Gets the information of all profiles on this interface.
        /// </summary>
        /// <returns>The profiles information.</returns>
        public Wlan.WlanProfileInfo[] GetProfiles()
        {
            IntPtr profileListPtr;
            Wlan.ThrowIfError(
                Wlan.WlanGetProfileList(m_Client.ClientHandle, m_Info.interfaceGuid, IntPtr.Zero, out profileListPtr));
            try
            {
                Wlan.WlanProfileInfoListHeader header = (Wlan.WlanProfileInfoListHeader)Marshal.PtrToStructure(profileListPtr, typeof(Wlan.WlanProfileInfoListHeader));
                Wlan.WlanProfileInfo[] profileInfos = new Wlan.WlanProfileInfo[header.numberOfItems];
                long profileListIterator = profileListPtr.ToInt64() + Marshal.SizeOf(header);
                for (int i = 0; i < header.numberOfItems; ++i)
                {
                    Wlan.WlanProfileInfo profileInfo = (Wlan.WlanProfileInfo)Marshal.PtrToStructure(new IntPtr(profileListIterator), typeof(Wlan.WlanProfileInfo));
                    profileInfos[i] = profileInfo;
                    profileListIterator += Marshal.SizeOf(profileInfo);
                }
                return profileInfos;
            }
            finally
            {
                Wlan.WlanFreeMemory(profileListPtr);
            }
        }

        internal void OnWlanConnection(Wlan.WlanNotificationData notifyData, Wlan.WlanConnectionNotificationData connNotifyData)
        {
            WlanConnectionNotification?.Invoke(notifyData, connNotifyData);

            if (m_QueueEvents)
            {
                WlanConnectionNotificationEventData queuedEvent = new WlanConnectionNotificationEventData
                {
                    NotifyData = notifyData,
                    ConnNotifyData = connNotifyData
                };
                EnqueueEvent(queuedEvent);
            }
        }

        internal void OnWlanMsm(Wlan.WlanNotificationData notifyData, Wlan.WlanMsmNotificationData msmNotifyData)
        {
            WlanMsmNotification?.Invoke(notifyData, msmNotifyData);
        }

        internal void OnWlanRadioStateChange(Wlan.WlanNotificationData notifyData, Wlan.WlanPhyRadioState phyRadioState)
        {
            WlanRadioStateChangedNotification?.Invoke(notifyData, phyRadioState);
        }

        internal void OnWlanReason(Wlan.WlanNotificationData notifyData, Wlan.WlanReasonCode reasonCode)
        {
            WlanReasonNotification?.Invoke(notifyData, reasonCode);
            if (m_QueueEvents)
            {
                WlanReasonNotificationData queuedEvent = new WlanReasonNotificationData
                {
                    NotifyData = notifyData,
                    ReasonCode = reasonCode
                };
                EnqueueEvent(queuedEvent);
            }
        }

        internal void OnWlanSignalQualityChange(Wlan.WlanNotificationData notificationData, Wlan.WlanSignalQuality signalQuality)
        {
            WlanSignalQualityChangedNotification?.Invoke(notificationData, signalQuality);
        }

        internal void OnWlanPowerSettingChange(Wlan.WlanNotificationData notificationData, Wlan.WlanPowerSetting powerSetting)
        {
            WlanPowerSettingChangedNotification?.Invoke(notificationData, powerSetting);
        }

        public void OnWlanScanComplete(Wlan.WlanNotificationData notifyData)
        {
            WlanScanCompleteNotification?.Invoke(notifyData);
        }

        public void OnScanFail(Wlan.WlanNotificationData notifyData, Wlan.WlanReasonCode reasonCode)
        {
            WlanScanFailNotification?.Invoke(notifyData, reasonCode);
        }

        internal void OnWlanNotification(Wlan.WlanNotificationData notifyData)
        {
            WlanNotification?.Invoke(notifyData);
        }

        /// <summary>
        /// Enqueues a notification event to be processed serially.
        /// </summary>
        private void EnqueueEvent(object queuedEvent)
        {
            lock (m_EventQueue)
                m_EventQueue.Enqueue(queuedEvent);
            m_EventQueueFilled.Set();
        }

        /// <summary>
        /// Gets the network interface of this wireless interface.
        /// </summary>
        /// <remarks>
        /// The network interface allows querying of generic network properties such as the interface's IP address.
        /// </remarks>
        public NetworkInterface NetworkInterface
        {
            get
            {
                // Do not cache the NetworkInterface; We need it fresh
                // each time cause otherwise it caches the IP information.
                foreach (NetworkInterface netIface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    Guid netIfaceGuid = new Guid(netIface.Id);
                    if (netIfaceGuid.Equals(m_Info.interfaceGuid))
                    {
                        return netIface;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// The GUID of the interface (same content as the <see cref="System.Net.NetworkInformation.NetworkInterface.Id"/> value).
        /// </summary>
        public Guid InterfaceGuid => m_Info.interfaceGuid;

        /// <summary>
        /// The description of the interface.
        /// This is a user-immutable string containing the vendor and model name of the adapter.
        /// </summary>
        public string InterfaceDescription => m_Info.interfaceDescription;

        /// <summary>
        /// The friendly name given to the interface by the user (e.g. "Local Area Network Connection").
        /// </summary>
        public string InterfaceName => NetworkInterface.Name;

        /// <summary>
        /// Sends a Proprietary method OID.
        /// Does not support Microsoft NDIS OIDs.
        /// 
        /// Possible native error codes:
        /// 234 : output buffer was too small
        /// 21 : device is not ready
        /// 22 : The device does not recognize the command.
        /// </summary>
        /// <param name="inputBuffer">the input buffer</param>
        /// <param name="outputBuffer">the output buffer</param>
        /// <param name="bytesWritten">the number of bytes written to the output buffer</param>
        public void IhvControl(byte[] inputBuffer, byte[] outputBuffer, out int bytesWritten)
        {
            int ret = Wlan.WlanIhvControl(m_Client.ClientHandle,
                InterfaceGuid,
                Wlan.WlanIhvControlType.wlan_ihv_control_type_driver,
                inputBuffer.Length,
                inputBuffer,
                outputBuffer.Length,
                outputBuffer,
                out bytesWritten);

            Wlan.ThrowIfError(ret);
        }
    }
}
