using System;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Java.Interop;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using XamarinForms.Core.Standard.Infrastructure.Interfaces;

[assembly:Xamarin.Forms.Dependency(typeof(DeviceInfo))] 
namespace XamarinForms.Core.Droid.Infrastructure
{
    public class DeviceInfo
    {
		public string Manufacturer => Build.Manufacturer;

		public string DeviceName
		{
			get
			{
				var name = Settings.System.GetString(Application.Context.ContentResolver, "device_name");
				
				if (string.IsNullOrWhiteSpace(name))
					name = Model;

				return name;
			}
		}

        public string Id
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_id))
                    return _id;

                _id = GetSerialField();
	            
                if(string.IsNullOrWhiteSpace(_id) || _id == Build.Unknown || _id == "0")
                {
                    try
                    {
//                        var context = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context; 
	                    var context =Application.Context;
                        _id = Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);
                    }
                    catch(Exception ex)
                    {
                        Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex.ToString());
                    }
                }

                return _id;
            }
        }

	    private string _id = string.Empty;

        public string Model => Build.Model;

        public string Version => Build.VERSION.Release; 

		public Version VersionNumber => Utils.ParseVersion(Version);

		/// <summary>
		/// Returns the current version of the app, as defined in the metadata, e.g. "4.3".
		/// </summary>
		/// <value>The current version.</value>
		public string AppVersion
		{
			get
			{
				using (var info = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, PackageInfoFlags.MetaData))
					return info.VersionName;
			}
		}


		public string AppBuild
		{
			get
			{
				using (var info = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, PackageInfoFlags.MetaData))
					return info.VersionCode.ToString();
			}
		}

        public bool IsDevice => !(
            Build.Fingerprint.StartsWith("generic", StringComparison.InvariantCulture)
            || Build.Fingerprint.StartsWith("unknown", StringComparison.InvariantCulture)
            || Build.Model.Contains("google_sdk")
            || Build.Model.Contains("Emulator")
            || Build.Model.Contains("Android SDK built for x86")
            || Build.Manufacturer.Contains("Genymotion")
            || (Build.Brand.StartsWith("generic", StringComparison.InvariantCulture) && Build.Device.StartsWith("generic", StringComparison.InvariantCulture))
            || Build.Product.Equals("google_sdk", StringComparison.InvariantCulture)
        );
	    
	    #region Fields

	    private static readonly JniPeerMembers BuildMembers = new XAPeerMembers("android/os/Build", typeof(Build));
	    
	    #endregion
	    
	    #region Private Methods
	    
	    private static string GetSerialField()
	    {
		    try
		    {
			    const string id = "SERIAL.Ljava/lang/String;";
			    var value = BuildMembers.StaticFields.GetObjectValue(id);
			    return JNIEnv.GetString(value.Handle, JniHandleOwnership.TransferLocalRef);
		    }
		    catch
		    {
			    return string.Empty;

		    }
	    }
	    
	    #endregion
    }
}