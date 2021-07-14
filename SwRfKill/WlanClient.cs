using Net;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Net
{
    /// <summary>
    /// Represents a client to the Zeroconf (Native Wifi) service.
    /// </summary>
    /// <remarks>
    /// This class is the entry point to Native Wifi management. To manage WiFi settings, create an instance
    /// of this class.
    /// </remarks>
    public class WlanClient : IWlanClient
    {
        public const string WlanServiceName = "WlanSvc";

        private IntPtr m_ClientHandle;

        internal IntPtr ClientHandle => m_ClientHandle;

        private readonly Dictionary<Guid, WlanInterface> m_Interfaces = new Dictionary<Guid, WlanInterface>();

        [SuppressMessage("ReSharper", "PrivateFieldCanBeConvertedToLocalVariable",
            Justification = "This field is required because we need to prevent the delegate from being garbage collected")]
        private readonly Wlan.WlanNotificationCallbackDelegate m_Callback;

        /// <summary>
        /// Creates a new instance of a Native Wifi service client.
        /// </summary>
        public WlanClient()
        {
            m_Callback = OnWlanNotification;
            RefreshHandle();
        }

        public void RefreshHandle()
        {
            Console.WriteLine("Initializing Wlan client handle");

            //var wlanService = new ServiceController(WlanServiceName);
            //if (wlanService.Status != ServiceControllerStatus.Running)
            //{
            //    Trace.WriteLine("WlanSvc isn't running. Starting service");
            //    wlanService.Start();
            //    Thread.Sleep(1000);
            //}

            m_ClientHandle = IntPtr.Zero;
            uint negotiatedVersion;
            Wlan.ThrowIfError(
                Wlan.WlanOpenHandle(Wlan.WLAN_CLIENT_VERSION_XP_SP2, IntPtr.Zero, out negotiatedVersion, out m_ClientHandle));
            try
            {
                Wlan.WlanNotificationSource prevSrc;
                Wlan.ThrowIfError(
                    Wlan.WlanRegisterNotification(m_ClientHandle, Wlan.WlanNotificationSource.All, false, m_Callback, IntPtr.Zero, IntPtr.Zero, out prevSrc));
            }
            catch
            {
                Close();
                throw;
            }
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
            Close();
        }

        ~WlanClient()
        {
            ((IDisposable)this).Dispose();
        }

        /// <summary>
        /// Closes the handle.
        /// </summary>
        private void Close()
        {
            if (m_ClientHandle != IntPtr.Zero)
            {
                Wlan.WlanCloseHandle(m_ClientHandle, IntPtr.Zero);
                m_ClientHandle = IntPtr.Zero;
            }
        }

        private static Wlan.WlanConnectionNotificationData? ParseWlanConnectionNotification(ref Wlan.WlanNotificationData notifyData)
        {
            int expectedSize = Marshal.SizeOf(typeof(Wlan.WlanConnectionNotificationData));
            if (notifyData.dataSize < expectedSize)
                return null;

            Wlan.WlanConnectionNotificationData connNotifyData =
                (Wlan.WlanConnectionNotificationData)
                Marshal.PtrToStructure(notifyData.dataPtr, typeof(Wlan.WlanConnectionNotificationData));
            if (connNotifyData.wlanReasonCode == Wlan.WlanReasonCode.Success)
            {
                long dataStart = notifyData.dataPtr.ToInt64();
                long offset = Marshal.OffsetOf(typeof(Wlan.WlanConnectionNotificationData), "profileXml").ToInt64();
                IntPtr profileXmlPtr = new IntPtr(dataStart + offset);
                connNotifyData.profileXml = Marshal.PtrToStringUni(profileXmlPtr);
            }

            return connNotifyData;
        }

        private static Wlan.WlanMsmNotificationData? ParseWlanMsmNotification(ref Wlan.WlanNotificationData notifyData)
        {
            int expectedSize = Marshal.SizeOf(typeof(Wlan.WlanMsmNotificationData));
            if (notifyData.dataSize < expectedSize)
                return null;

            Wlan.WlanMsmNotificationData msmNotifyData =
                (Wlan.WlanMsmNotificationData)
                Marshal.PtrToStructure(notifyData.dataPtr, typeof(Wlan.WlanMsmNotificationData));

            return msmNotifyData;
        }

        private static Wlan.WlanPhyRadioState? ParseWlanRadioStateNotification(ref Wlan.WlanNotificationData notifyData)
        {
            int expectedSize = Marshal.SizeOf(typeof(Wlan.WlanPhyRadioState));
            if (notifyData.dataSize < expectedSize)
                return null;

            Wlan.WlanPhyRadioState radionStateData =
                (Wlan.WlanPhyRadioState)
                Marshal.PtrToStructure(notifyData.dataPtr, typeof(Wlan.WlanPhyRadioState));

            return radionStateData;
        }


        private static Wlan.WlanSignalQuality? ParseWlanSignalQualityNotification(ref Wlan.WlanNotificationData notifyData)
        {
            int expectedSize = Marshal.SizeOf(typeof(Wlan.WlanSignalQuality));
            if (notifyData.dataSize < expectedSize)
                return null;

            Wlan.WlanSignalQuality signalQuality =
                (Wlan.WlanSignalQuality)
                Marshal.PtrToStructure(notifyData.dataPtr, typeof(Wlan.WlanSignalQuality));

            return signalQuality;
        }

        /// <summary>
        /// Called when a wlan notification is received from the OS.
        /// all notifications that have data structure are mapped to unique events.
        /// based on: https://msdn.microsoft.com/en-us/library/windows/desktop/ms706902(v=vs.85).aspx
        /// </summary>
        /// <param name="notifyData">The notify data.</param>
        /// <param name="context">The context.</param>
        private void OnWlanNotification(ref Wlan.WlanNotificationData notifyData, IntPtr context)
        {
            WlanInterface wlanIface;
            m_Interfaces.TryGetValue(notifyData.interfaceGuid, out wlanIface);

            switch (notifyData.notificationSource)
            {
                case Wlan.WlanNotificationSource.ACM:
                    switch ((Wlan.WlanNotificationCodeAcm)notifyData.notificationCode)
                    {
                        case Wlan.WlanNotificationCodeAcm.ConnectionStart:
                        case Wlan.WlanNotificationCodeAcm.ConnectionComplete:
                        case Wlan.WlanNotificationCodeAcm.ConnectionAttemptFail:
                        case Wlan.WlanNotificationCodeAcm.Disconnecting:
                        case Wlan.WlanNotificationCodeAcm.Disconnected:
                            Wlan.WlanConnectionNotificationData? connNotifyData = ParseWlanConnectionNotification(ref notifyData);
                            if (connNotifyData.HasValue)
                            {
                                wlanIface?.OnWlanConnection(notifyData, connNotifyData.Value);
                            }
                            break;
                        case Wlan.WlanNotificationCodeAcm.ScanFail:
                            {
                                int expectedSize = Marshal.SizeOf(typeof(int));
                                if (notifyData.dataSize >= expectedSize)
                                {
                                    Wlan.WlanReasonCode reasonCode = (Wlan.WlanReasonCode)Marshal.ReadInt32(notifyData.dataPtr);
                                    wlanIface?.OnScanFail(notifyData, reasonCode);
                                }
                            }
                            break;
                        case Wlan.WlanNotificationCodeAcm.PowerSettingChange:
                            {
                                int expectedSize = Marshal.SizeOf(Enum.GetUnderlyingType(typeof(Wlan.WlanPowerSetting)));
                                if (notifyData.dataSize >= expectedSize)
                                {
                                    Wlan.WlanPowerSetting powerSetting = (Wlan.WlanPowerSetting)Marshal.ReadInt32(notifyData.dataPtr);
                                    wlanIface?.OnWlanPowerSettingChange(notifyData, powerSetting);
                                }
                            }
                            break;
                        case Wlan.WlanNotificationCodeAcm.ScanComplete:
                            {
                                wlanIface?.OnWlanScanComplete(notifyData);
                            }
                            break;
                    }
                    break;
                case Wlan.WlanNotificationSource.MSM:
                    switch ((Wlan.WlanNotificationCodeMsm)notifyData.notificationCode)
                    {
                        case Wlan.WlanNotificationCodeMsm.Associating:
                        case Wlan.WlanNotificationCodeMsm.Associated:
                        case Wlan.WlanNotificationCodeMsm.Authenticating:
                        case Wlan.WlanNotificationCodeMsm.Connected:
                        case Wlan.WlanNotificationCodeMsm.Disassociating:
                        case Wlan.WlanNotificationCodeMsm.Disconnected:
                        case Wlan.WlanNotificationCodeMsm.PeerJoin:
                        case Wlan.WlanNotificationCodeMsm.PeerLeave:
                        case Wlan.WlanNotificationCodeMsm.AdapterRemoval:
                        case Wlan.WlanNotificationCodeMsm.RoamingStart:
                        case Wlan.WlanNotificationCodeMsm.RoamingEnd:
                            {
                                Wlan.WlanMsmNotificationData? connNotifyData = ParseWlanMsmNotification(ref notifyData);
                                if (connNotifyData.HasValue)
                                {
                                    wlanIface?.OnWlanMsm(notifyData, connNotifyData.Value);
                                }
                                break;
                            }
                        case Wlan.WlanNotificationCodeMsm.RadioStateChange:
                            Wlan.WlanPhyRadioState? radionStateData = ParseWlanRadioStateNotification(ref notifyData);
                            if (radionStateData.HasValue)
                            {
                                wlanIface?.OnWlanRadioStateChange(notifyData, radionStateData.Value);
                            }
                            break;
                        case Wlan.WlanNotificationCodeMsm.SignalQualityChange:
                            Wlan.WlanSignalQuality? signalQualityData = ParseWlanSignalQualityNotification(ref notifyData);
                            if (signalQualityData.HasValue)
                            {
                                wlanIface?.OnWlanSignalQualityChange(notifyData, signalQualityData.Value);
                            }
                            break;
                    }
                    break;
            }

            wlanIface?.OnWlanNotification(notifyData);
        }


        /// <summary>
        /// Gets the WLAN interfaces.
        /// </summary>
        /// <value>The WLAN interfaces.</value>
        public IWlanInterface[] Interfaces
        {
            get
            {
                IntPtr ifaceList;
                Wlan.ThrowIfError(
                    Wlan.WlanEnumInterfaces(ClientHandle, IntPtr.Zero, out ifaceList));
                try
                {
                    Wlan.WlanInterfaceInfoListHeader header =
                        (Wlan.WlanInterfaceInfoListHeader)Marshal.PtrToStructure(ifaceList, typeof(Wlan.WlanInterfaceInfoListHeader));
                    long listIterator = ifaceList.ToInt64() + Marshal.SizeOf(header);
                    IWlanInterface[] interfaces = new IWlanInterface[header.numberOfItems];
                    var currentIfaceGuids = new List<Guid>();
                    for (int i = 0; i < header.numberOfItems; ++i)
                    {
                        Wlan.WlanInterfaceInfo info =
                            (Wlan.WlanInterfaceInfo)Marshal.PtrToStructure(new IntPtr(listIterator), typeof(Wlan.WlanInterfaceInfo));
                        listIterator += Marshal.SizeOf(info);
                        currentIfaceGuids.Add(info.interfaceGuid);

                        WlanInterface wlanIface;
                        if (!m_Interfaces.TryGetValue(info.interfaceGuid, out wlanIface))
                        {
                            wlanIface = new WlanInterface(this, info);
                            m_Interfaces[info.interfaceGuid] = wlanIface;
                        }

                        interfaces[i] = wlanIface;
                    }

                    // Remove stale interfaces
                    var deadIfacesGuids = new Queue<Guid>();
                    foreach (Guid ifaceGuid in m_Interfaces.Keys)
                    {
                        if (!currentIfaceGuids.Contains(ifaceGuid))
                            deadIfacesGuids.Enqueue(ifaceGuid);
                    }
                    while (deadIfacesGuids.Count != 0)
                    {
                        Guid deadIfaceGuid = deadIfacesGuids.Dequeue();
                        m_Interfaces.Remove(deadIfaceGuid);
                    }

                    return interfaces;
                }
                finally
                {
                    Wlan.WlanFreeMemory(ifaceList);
                }
            }
        }
    }
}