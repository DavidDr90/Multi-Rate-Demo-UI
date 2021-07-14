using System;

namespace Net
{
    public interface IWlanClient : IDisposable
    {
        /// <summary>
        /// Gets the WLAN interfaces.
        /// </summary>
        /// <value>The WLAN interfaces.</value>
        IWlanInterface[] Interfaces { get; }

        /// <summary>
        /// Refresh the WlanHandle of the client (After handle was killed).
        /// </summary>
        void RefreshHandle();
    }
}