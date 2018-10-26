using System;

namespace Xamarin.Core.Standard.Infrastructure.Settings
{
    public interface ISettings
    {

        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>Value or default</returns>
        decimal GetValueOrDefault(string key, decimal defaultValue, string fileName = null);
        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>Value or default</returns>
        bool GetValueOrDefault(string key, bool defaultValue, string fileName = null);
        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>Value or default</returns>
        long GetValueOrDefault(string key, long defaultValue, string fileName = null);
        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>Value or default</returns>
        string GetValueOrDefault(string key, string defaultValue, string fileName = null);
        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>Value or default</returns>
        int GetValueOrDefault(string key, int defaultValue, string fileName = null);
        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>Value or default</returns>
        float GetValueOrDefault(string key, float defaultValue, string fileName = null);
        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>Value or default</returns>
        DateTime GetValueOrDefault(string key, DateTime defaultValue, string fileName = null);
        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>Value or default</returns>
        Guid GetValueOrDefault(string key, Guid defaultValue, string fileName = null);
        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>Value or default</returns>
        double GetValueOrDefault(string key, double defaultValue, string fileName = null);


        /// <summary>
        /// Adds or updates the value 
        /// </summary>
        /// <param name="key">Key for settting</param>
        /// <param name="value">Value to set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True of was added or updated and you need to save it.</returns>
        bool AddOrUpdateValue(string key, decimal value, string fileName = null);
        /// <summary>
        /// Adds or updates the value 
        /// </summary>
        /// <param name="key">Key for settting</param>
        /// <param name="value">Value to set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True of was added or updated and you need to save it.</returns>
        bool AddOrUpdateValue(string key, bool value, string fileName = null);
        /// <summary>
        /// Adds or updates the value 
        /// </summary>
        /// <param name="key">Key for settting</param>
        /// <param name="value">Value to set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True of was added or updated and you need to save it.</returns>
        bool AddOrUpdateValue(string key, long value, string fileName = null);
        /// <summary>
        /// Adds or updates the value 
        /// </summary>
        /// <param name="key">Key for settting</param>
        /// <param name="value">Value to set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True of was added or updated and you need to save it.</returns>
        bool AddOrUpdateValue(string key, string value, string fileName = null);
        /// <summary>
        /// Adds or updates the value 
        /// </summary>
        /// <param name="key">Key for settting</param>
        /// <param name="value">Value to set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True of was added or updated and you need to save it.</returns>
        bool AddOrUpdateValue(string key, int value, string fileName = null);
        /// <summary>
        /// Adds or updates the value 
        /// </summary>
        /// <param name="key">Key for settting</param>
        /// <param name="value">Value to set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True of was added or updated and you need to save it.</returns>
        bool AddOrUpdateValue(string key, float value, string fileName = null);
        /// <summary>
        /// Adds or updates the value 
        /// </summary>
        /// <param name="key">Key for settting</param>
        /// <param name="value">Value to set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True of was added or updated and you need to save it.</returns>
        bool AddOrUpdateValue(string key, DateTime value, string fileName = null);
        /// <summary>
        /// Adds or updates the value 
        /// </summary>
        /// <param name="key">Key for settting</param>
        /// <param name="value">Value to set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True of was added or updated and you need to save it.</returns>
        bool AddOrUpdateValue(string key, Guid value, string fileName = null);
        /// <summary>
        /// Adds or updates the value 
        /// </summary>
        /// <param name="key">Key for settting</param>
        /// <param name="value">Value to set</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True of was added or updated and you need to save it.</returns>
        bool AddOrUpdateValue(string key, double value, string fileName = null);


        /// <summary>
        /// Removes a desired key from the settings
        /// </summary>
        /// <param name="key">Key for setting</param>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        void Remove(string key, string fileName = null);

        /// <summary>
        /// Clear all keys from settings
        /// </summary>
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        void Clear(string fileName = null);

        /// <summary>
        /// Checks to see if the key has been added.
        /// </summary>
        /// <param name="key">Key to check</param> 
        /// <param name="fileName">Name of file for settings to be stored and retrieved </param>
        /// <returns>True if contains key, else false</returns>
        bool Contains(string key, string fileName = null);

        /// <summary>
        /// Attempts to open the app settings page.
        /// </summary>
        /// <returns>true if success, else false and not supported</returns>
        bool OpenAppSettings();
    }
}