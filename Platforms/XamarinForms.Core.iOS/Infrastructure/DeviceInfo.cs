using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms.Internals;
using XamarinForms.Core.Standard.Infrastructure.Interfaces;

[assembly:Xamarin.Forms.Dependency(typeof(DeviceInfo))] 
namespace XamarinForms.Core.iOS.Infrastructure
{
	public sealed class DeviceInfo : IDeviceInfo
	{
		public string Manufacturer => "Apple";

		public string DeviceName => UIDevice.CurrentDevice.Name;

		public string Id => UIDevice.CurrentDevice.IdentifierForVendor.AsString();

		public string Model => UIDevice.CurrentDevice.Model;

		public string Version => UIDevice.CurrentDevice.SystemVersion;

		public Version VersionNumber => Utils.ParseVersion(Version);

		public string AppVersion => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();

		public string AppBuild => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();

		public bool IsDevice => Runtime.Arch == Arch.DEVICE;
	}
}