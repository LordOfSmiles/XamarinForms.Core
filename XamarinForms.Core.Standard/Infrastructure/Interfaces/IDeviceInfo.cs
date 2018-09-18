using System;

namespace XamarinForms.Core.Standard.Infrastructure.Interfaces
{
    public interface IDeviceInfo
    {
        /// <summary>
        /// This is the device specific Id (remember the correct permissions in your app to use this)
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Get the model of the device
        /// </summary>
        string Model { get; }

		/// <summary>
		/// Get the name of the device
		/// </summary>
		string Manufacturer { get; }

		/// <summary>
		/// Get the name of the device
		/// </summary>
		string DeviceName { get; }

		/// <summary>
		/// Gets the version of the operating system as a string
		/// </summary>
		string Version { get; }

        /// <summary>
        /// Gets the version number of the operating system as a <see cref="Version"/>
        /// </summary>
        Version VersionNumber { get; }

		/// <summary>
		/// Returns the current version of the app, as defined in the PList, e.g. "4.3".
		/// </summary>
		/// <value>The current version.</value>
		string AppVersion { get; }

		/// <summary>
		/// Returns the current build of the app, as defined in the PList, e.g. "4300".
		/// </summary>
		/// <value>The current build.</value>
		string AppBuild { get; }

        /// <summary>
        /// Checks whether this is a real device or an emulator/simulator
        /// </summary>
        bool IsDevice { get; }
    }
	
	public class Utils
	{
		public static Version ParseVersion(string version)
		{
			if (Version.TryParse(version, out var number))
				return number;

			if (int.TryParse(version, out var major))
				return new Version(major, 0);

			return new Version(0, 0);
		}
	}
}