using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using SystemConfiguration;
using CoreFoundation;

namespace XamarinForms.Core.iOS.Infrastructure.Connectivity
{
     public enum NetworkStatus
    {
        /// <summary>
        /// No internet connection
        /// </summary>
        NotReachable,
        /// <summary>
        /// Reachable view Cellular.
        /// </summary>
        ReachableViaCarrierDataNetwork,
        /// <summary>
        /// Reachable view wifi
        /// </summary>
        ReachableViaWiFiNetwork
    }

    /// <summary>
    /// Reachability helper
    /// </summary>
    [Foundation.Preserve(AllMembers = true)]
    public static class Reachability
    {
        /// <summary>
        /// Default host name to use
        /// </summary>
        public static string HostName = "www.google.com";

        /// <summary>
        /// Checks if reachable without requiring a connection
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
        {
            // Is it reachable with the current network configuration?
            bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

            // Do we need a connection to reach it?
            bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

#if __IOS__
            // Since the network stack will automatically try to get the WAN up,
            // probe that
            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                noConnectionRequired = true;
#endif
            return isReachable && noConnectionRequired;
        }

        /// <summary>
        /// Checks if host is reachable
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool IsHostReachable(string host, int port)
        {
            if (string.IsNullOrWhiteSpace(host))
                return false;

            IPAddress address;
            if (!IPAddress.TryParse(host + ":" + port, out address))
            {
                Debug.WriteLine(host + ":" + port + " is not valid");
                return false;
            }
            using (var r = new NetworkReachability(host))
            {

                NetworkReachabilityFlags flags;

                if (r.TryGetFlags(out flags))
                {
                    return IsReachableWithoutRequiringConnection(flags);
                }
            }
            return false;
        }

        /// <summary>
        ///  Is the host reachable with the current network configuration
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static bool IsHostReachable(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
                return false;

            using (var r = new NetworkReachability(host))
            {

                NetworkReachabilityFlags flags;

                if (r.TryGetFlags(out flags))
                {
                    return IsReachableWithoutRequiringConnection(flags);
                }
            }
            return false;
        }


        /// <summary>
        /// Raised every time there is an interesting reachable event,
        /// we do not even pass the info as to what changed, and
        /// we lump all three status we probe into one
        /// </summary>
        public static event EventHandler ReachabilityChanged;

        static async void OnChange(NetworkReachabilityFlags flags)
        {
            await Task.Delay(100);
            ReachabilityChanged?.Invoke(null, EventArgs.Empty);

        }


        /*Removal of reachabilityForLocalWiFi
		============
		Older versions of this sample included the method reachabilityForLocalWiFi.As originally designed, this method allowed apps using Bonjour to check the status of "local only" Wi-Fi(Wi-Fi without a connection to the larger internet) to determine whether or not they should advertise or browse.
		However, the additional peer-to-peer APIs that have since been added to iOS and OS X have rendered it largely obsolete.Because of the narrow use case for this API and the large potential for misuse, reachabilityForLocalWiFi has been removed from Reachability.
		Apps that have a specific requirement can use reachabilityWithAddress to monitor IN_LINKLOCALNETNUM (that is, 169.254.0.0).  
		 
		Note: ONLY apps that have a specific requirement should be monitoring IN_LINKLOCALNETNUM.For the overwhelming majority of apps, monitoring this address is unnecessary and potentially harmful.
		*/
        // Returns true if it is possible to reach the AdHoc WiFi network
        // and optionally provides extra network reachability flags as the
        // out parameter
        //
        //
        //static NetworkReachability adHocWiFiNetworkReachability;
        /// <summary>
        /// Checks ad hoc wifi is available
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        /*public static bool IsAdHocWiFiNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (adHocWiFiNetworkReachability == null)
            {
                //var ip = IPAddress.Parse("::ffff:169.254.0.0");
                //var data = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 169, 254, 0, 0 };
                var ip = new IPAddress(new byte[] { 169, 254, 0, 0 });
                adHocWiFiNetworkReachability = new NetworkReachability(ip);
                adHocWiFiNetworkReachability.SetNotification(OnChange);
                adHocWiFiNetworkReachability.Schedule(CFRunLoop.Main, CFRunLoop.ModeDefault);
            }
            if (!adHocWiFiNetworkReachability.TryGetFlags(out flags))
                return false;
            return IsReachableWithoutRequiringConnection(flags);
        }*/



        static NetworkReachability defaultRouteReachability;
        static bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
        {

            if (defaultRouteReachability == null)
            {
                var ip = new IPAddress(0);
                defaultRouteReachability = new NetworkReachability(ip);
                defaultRouteReachability.SetNotification(OnChange);
                defaultRouteReachability.Schedule(CFRunLoop.Main, CFRunLoop.ModeDefault);
            }
            if (!defaultRouteReachability.TryGetFlags(out flags))
                return false;
            return IsReachableWithoutRequiringConnection(flags);
        }

        static NetworkReachability remoteHostReachability;
        /// <summary>
        /// Checks the remote host status
        /// </summary>
        /// <returns></returns>
        public static NetworkStatus RemoteHostStatus()
        {
            NetworkReachabilityFlags flags;
            bool reachable;

            if (remoteHostReachability == null)
            {
                remoteHostReachability = new NetworkReachability(HostName);

                // Need to probe before we queue, or we wont get any meaningful values
                // this only happens when you create NetworkReachability from a hostname
                reachable = remoteHostReachability.TryGetFlags(out flags);

                remoteHostReachability.SetNotification(OnChange);
                remoteHostReachability.Schedule(CFRunLoop.Main, CFRunLoop.ModeDefault);
            }
            else
                reachable = remoteHostReachability.TryGetFlags(out flags);

            if (!reachable)
                return NetworkStatus.NotReachable;

            if (!IsReachableWithoutRequiringConnection(flags))
                return NetworkStatus.NotReachable;

#if __IOS__
            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                return NetworkStatus.ReachableViaCarrierDataNetwork;
#endif

            return NetworkStatus.ReachableViaWiFiNetwork;
        }

        /// <summary>
        /// Checks internet connection status
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<NetworkStatus> GetActiveConnectionType()
        {
            var status = new List<NetworkStatus>();

            NetworkReachabilityFlags flags;
            bool defaultNetworkAvailable = IsNetworkAvailable(out flags);

#if __IOS__
            // If it's a WWAN connection..
            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                status.Add(NetworkStatus.ReachableViaCarrierDataNetwork);
            else if (defaultNetworkAvailable)
#else
            // If the connection is reachable and no connection is required, then assume it's WiFi
            if (defaultNetworkAvailable)
#endif

            {
                status.Add(NetworkStatus.ReachableViaWiFiNetwork);
			}
            else if (((flags & NetworkReachabilityFlags.ConnectionOnDemand) != 0
				|| (flags & NetworkReachabilityFlags.ConnectionOnTraffic) != 0)
				&& (flags & NetworkReachabilityFlags.InterventionRequired) == 0)
			{
				// If the connection is on-demand or on-traffic and no user intervention
				// is required, then assume WiFi.
                status.Add(NetworkStatus.ReachableViaWiFiNetwork);
			}



			return status;
		}

        /// <summary>
        /// Checks internet connection status
        /// </summary>
        /// <returns></returns>
        public static NetworkStatus InternetConnectionStatus()
        {
            NetworkStatus status = NetworkStatus.NotReachable;

            NetworkReachabilityFlags flags;
            bool defaultNetworkAvailable = IsNetworkAvailable(out flags);

#if __IOS__
			// If it's a WWAN connection..
			if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
				status = NetworkStatus.ReachableViaCarrierDataNetwork;
#endif

			// If the connection is reachable and no connection is required, then assume it's WiFi
			if (defaultNetworkAvailable)
            {
                status = NetworkStatus.ReachableViaWiFiNetwork;
            }

            // If the connection is on-demand or on-traffic and no user intervention
            // is required, then assume WiFi.
            if (((flags & NetworkReachabilityFlags.ConnectionOnDemand) != 0
                || (flags & NetworkReachabilityFlags.ConnectionOnTraffic) != 0)
                && (flags & NetworkReachabilityFlags.InterventionRequired) == 0)
            {
                status = NetworkStatus.ReachableViaWiFiNetwork;
            }


            return status;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public static void Dispose()
        {
            if (remoteHostReachability != null)
            {
                remoteHostReachability.Dispose();
                remoteHostReachability = null;
            }

            if (defaultRouteReachability != null)
            {
                defaultRouteReachability.Dispose();
                defaultRouteReachability = null;
            }
        }

    }
}