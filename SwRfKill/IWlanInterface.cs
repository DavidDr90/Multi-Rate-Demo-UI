using System;
using System.Net.NetworkInformation;

namespace Net
{
    public interface IWlanInterface
    {
        /// <summary>
        /// Occurs when an event of any kind occurs on a WLAN interface.
        /// </summary>
        event WlanInterface.WlanNotificationEventHandler WlanNotification;

        /// <summary>
        /// Occurs when a WLAN interface changes connection state.
        /// </summary>
        event WlanInterface.WlanConnectionNotificationEventHandler WlanConnectionNotification;

        /// <summary>
        /// Occurs when a WLAN interface changes MSM state.
        /// </summary>
        event WlanInterface.WlanMsmNotificationEventHandler WlanMsmNotification;

        /// <summary>
        /// Occurs when a WLAN operation fails due to some reason.
        /// </summary>
        event WlanInterface.WlanReasonNotificationEventHandler WlanReasonNotification;

        /// <summary>
        /// Occurs when the radio state changed.
        /// </summary>
        event WlanInterface.WlanRadioStateChangedEventHandler WlanRadioStateChangedNotification;

        /// <summary>
        /// Occurs when the radio state changed.
        /// </summary>
        event WlanInterface.WlanSignalQualityChangedEventHandler WlanSignalQualityChangedNotification;

        /// <summary>
        /// Occurs when the power setting changed.
        /// </summary>
        event WlanInterface.WlanPowerSettingChangedEventHandler WlanPowerSettingChangedNotification;

        /// <summary>
        /// Occurs when scan is completed successfully.
        /// </summary>
        event WlanInterface.WlanNotificationEventHandler WlanScanCompleteNotification;

        /// <summary>
        /// Occurs when scan is completed successfully.
        /// </summary>
        event WlanInterface.WlanReasonNotificationEventHandler WlanScanFailNotification;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WlanInterface"/> is automatically configured.
        /// </summary>
        /// <value><c>true</c> if "autoconf" is enabled; otherwise, <c>false</c>.</value>
        bool Autoconf { get; set; }

        /// <summary>
        /// Gets or sets the BSS type for the indicated interface.
        /// </summary>
        /// <value>The type of the BSS.</value>
        Wlan.Dot11BssType BssType { get; set; }

        /// <summary>
        /// Gets the state of the interface.
        /// </summary>
        /// <value>The state of the interface.</value>
        Wlan.WlanInterfaceState InterfaceState { get; }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        /// <value>The channel.</value>
        /// <remarks>Not supported on Windows XP SP2.</remarks>
        int Channel { get; }

        /// <summary>
        /// Gets the RSSI.
        /// </summary>
        /// <value>The RSSI.</value>
        /// <remarks>Not supported on Windows XP SP2.</remarks>
        int Rssi { get; }

        /// <summary>
        /// Gets the capability.
        /// </summary>
        Wlan.WlanInterfaceCapability InterfaceCapability { get; }

        /// <summary>
        /// Gets the radio state.
        /// </summary>
        /// <value>The radio state.</value>
        /// <remarks>Not supported on Windows XP.</remarks>
        Wlan.WlanRadioState RadioState { get; }

        /// <summary>
        /// Gets the current operation mode.
        /// </summary>
        /// <value>The current operation mode.</value>
        /// <remarks>Not supported on Windows XP SP2.</remarks>
        Wlan.Dot11OperationMode CurrentOperationMode { get; }

        /// <summary>
        /// Gets the attributes of the current connection.
        /// </summary>
        /// <value>The current connection attributes.</value>
        /// <exception cref="Win32Exception">An exception with code 0x0000139F (The group or resource is not in the correct state to perform the requested operation.) will be thrown if the interface is not connected to a network.</exception>
        Wlan.WlanConnectionAttributes CurrentConnection { get; }

        /// <summary>
        /// Gets the network interface of this wireless interface.
        /// </summary>
        /// <remarks>
        /// The network interface allows querying of generic network properties such as the interface's IP address.
        /// </remarks>
        NetworkInterface NetworkInterface { get; }

        /// <summary>
        /// The GUID of the interface (same content as the <see cref="System.Net.NetworkInformation.NetworkInterface.Id"/> value).
        /// </summary>
        Guid InterfaceGuid { get; }

        /// <summary>
        /// The description of the interface.
        /// This is a user-immutable string containing the vendor and model name of the adapter.
        /// </summary>
        string InterfaceDescription { get; }

        /// <summary>
        /// The friendly name given to the interface by the user (e.g. "Local Area Network Connection").
        /// </summary>
        string InterfaceName { get; }

        /// <summary>
        /// Sets the state of a single phy radio.
        /// </summary>
        /// <param name="phyRadioState">State of the phy radio.</param>
        void SetPhyRadioState(Wlan.WlanPhyRadioState phyRadioState);

        /// <summary>
        /// Requests a scan for available networks.
        /// </summary>
        /// <remarks>
        /// The method returns immediately. Progress is reported through the <see cref="WlanInterface.WlanNotification"/> event.
        /// </remarks>
        void Scan();

        /// <summary>
        /// Retrieves the list of available networks.
        /// </summary>
        /// <param name="flags">Controls the type of networks returned.</param>
        /// <returns>A list of the available networks.</returns>
        Wlan.WlanAvailableNetwork[] GetAvailableNetworkList(Wlan.WlanGetAvailableNetworkFlags flags);

        /// <summary>
        /// Retrieves the basic service sets (BSS) list of all available networks.
        /// </summary>
        Wlan.WlanBssEntry[] GetNetworkBssList();

        /// <summary>
        /// Retrieves the basic service sets (BSS) list of the specified network.
        /// </summary>
        /// <param name="ssid">Specifies the SSID of the network from which the BSS list is requested.</param>
        /// <param name="bssType">Indicates the BSS type of the network.</param>
        /// <param name="securityEnabled">Indicates whether security is enabled on the network.</param>
        Wlan.WlanBssEntry[] GetNetworkBssList(Wlan.Dot11Ssid ssid, Wlan.Dot11BssType bssType, bool securityEnabled);

        /// <summary>
        /// Requests a connection (association) to the specified wireless network.
        /// </summary>
        /// <remarks>
        /// The method returns immediately. Progress is reported through the <see cref="WlanInterface.WlanNotification"/> event.
        /// </remarks>
        void Connect(Wlan.WlanConnectionMode connectionMode, Wlan.Dot11BssType bssType, string profile);

        /// <summary>
        /// Disconnects from the network.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Connects (associates) to the specified wireless network, returning either on a success to connect
        /// or a failure.
        /// </summary>
        /// <param name="connectionMode"></param>
        /// <param name="bssType"></param>
        /// <param name="profile"></param>
        /// <param name="connectTimeout"></param>
        /// <returns></returns>
        bool ConnectSynchronously(Wlan.WlanConnectionMode connectionMode, Wlan.Dot11BssType bssType, string profile, int connectTimeout);

        /// <summary>
        /// Connects to the specified wireless network.
        /// </summary>
        /// <remarks>
        /// The method returns immediately. Progress is reported through the <see cref="WlanInterface.WlanNotification"/> event.
        /// </remarks>
        void Connect(Wlan.WlanConnectionMode connectionMode, Wlan.Dot11BssType bssType, Wlan.Dot11Ssid ssid, Wlan.WlanConnectionFlags flags);

        /// <summary>
        /// Deletes a profile.
        /// </summary>
        /// <param name="profileName">
        /// The name of the profile to be deleted. Profile names are case-sensitive.
        /// On Windows XP SP2, the supplied name must match the profile name derived automatically from the SSID of the network. For an infrastructure network profile, the SSID must be supplied for the profile name. For an ad hoc network profile, the supplied name must be the SSID of the ad hoc network followed by <c>-adhoc</c>.
        /// </param>
        void DeleteProfile(string profileName);

        /// <summary>
        /// Sets the profile.
        /// </summary>
        /// <param name="flags">The flags to set on the profile.</param>
        /// <param name="profileXml">The XML representation of the profile. On Windows XP SP 2, special care should be taken to adhere to its limitations.</param>
        /// <param name="overwrite">If a profile by the given name already exists, then specifies whether to overwrite it (if <c>true</c>) or return an error (if <c>false</c>).</param>
        /// <returns>The resulting code indicating a success or the reason why the profile wasn't valid.</returns>
        Wlan.WlanReasonCode SetProfile(Wlan.WlanProfileFlags flags, string profileXml, bool overwrite);

        /// <summary>
        /// Gets the profile's XML specification.
        /// </summary>
        /// <param name="profileName">The name of the profile.</param>
        /// <returns>The XML document.</returns>
        string GetProfileXml(string profileName);

        /// <summary>
        /// Gets the information of all profiles on this interface.
        /// </summary>
        /// <returns>The profiles information.</returns>
        Wlan.WlanProfileInfo[] GetProfiles();

        /// <summary>
        /// Sends a Proprietary method OID.
        /// Does not support Microsoft NDIS OIDs.
        /// </summary>
        /// <exception cref="Win32Exception"/>
        /// <param name="inputBuffer">the input buffer</param>
        /// <param name="outputBuffer">the output buffer</param>
        /// <param name="bytesWritten">the number of bytes written to the output buffer</param>
        void IhvControl(byte[] inputBuffer, byte[] outputBuffer, out int bytesWritten);
    }
}