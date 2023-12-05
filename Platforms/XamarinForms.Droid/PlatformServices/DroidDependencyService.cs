using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Provider;
using AndroidX.Core.App;
using Java.IO;
using Xamarin.Essentials;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.PlatformServices;
using File = Java.IO.File;

namespace XamarinForms.Droid.PlatformServices;

public sealed class DroidDependencyService : IDroidDependencyService
{
    public bool CanUseExactsAlarms
    {
        get
        {
            bool result;

            if (VersionHelper.IsEqualOrGreater(12))
            {
                var alarmManager = Platform.AppContext.GetSystemService(Context.AlarmService) as AlarmManager;
                result = alarmManager?.CanScheduleExactAlarms() ?? false;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }

    public void GoToPowerSettings()
    {
        if (VersionHelper.IsEqualOrGreater(6))
        {
            var intent = new Intent();
            if (IsInPowerWhiteList)
            {
                intent.SetAction(Settings.ActionIgnoreBatteryOptimizationSettings);
            }
            else
            {
                intent.SetAction(Settings.ActionRequestIgnoreBatteryOptimizations);
                intent.SetData(Uri.Parse("package:" + AppInfo.PackageName));
            }

            try
            {
                Platform.CurrentActivity.StartActivity(intent);
            }
            catch
            {
                //
            }
        }
    }

    public bool IsInPowerWhiteList
    {
        get
        {
            var result = true;

            if (VersionHelper.IsEqualOrGreater(6))
            {
                var powerManager = (PowerManager)Platform.AppContext.GetSystemService(Context.PowerService);
                if (powerManager != null)
                {
                    result = powerManager.IsIgnoringBatteryOptimizations(AppInfo.PackageName);
                }
            }

            return result;
        }
    }

    public async Task<bool> SaveToDownloadsAsync(string filePath)
    {
        bool isSuccess;

        if (VersionHelper.IsEqualOrGreater(10))
        {
            isSuccess = await Api29ImplementationAsync(filePath);
        }
        else
        {
            isSuccess = await OldImplementationAsync(filePath);
        }

        return isSuccess;
    }

    #region Private Methods

    private static async Task<bool> Api29ImplementationAsync(string filePath)
    {
        var isSuccess = false;

        if (System.IO.File.Exists(filePath))
        {
            var fileName = Path.GetFileName(filePath);

            var contentValues = new ContentValues();
            contentValues.Put(MediaStore.IMediaColumns.Title, fileName);
            contentValues.Put(MediaStore.Downloads.InterfaceConsts.DisplayName, fileName);
            contentValues.Put(MediaStore.IMediaColumns.MimeType, "application/octet-stream");

            var length = new System.IO.FileInfo(filePath).Length;
            contentValues.Put(MediaStore.IMediaColumns.Size, length);

            // If you downloaded to a specific folder inside "Downloads" folder
            contentValues.Put(MediaStore.IMediaColumns.RelativePath, Environment.DirectoryDownloads);

            try
            {
                var contentResolver = Platform.AppContext.ContentResolver;
                var newUri = contentResolver?.Insert(MediaStore.Downloads.ExternalContentUri, contentValues);
                if (newUri != null)
                {
                    var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                    var saveStream = contentResolver.OpenOutputStream(newUri);
                    if (saveStream != null)
                    {
                        await saveStream.WriteAsync(bytes);
                        saveStream.Close();

                        isSuccess = true;
                    }
                }
            }
            catch
            {
                //
            }
        }

        return isSuccess;
    }

    private static async Task<bool> OldImplementationAsync(string filePath)
    {
        var isSuccess = false;
        
        try
        {
            if (System.IO.File.Exists(filePath))
            {
                var downloadsFolder = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads);
                if (downloadsFolder != null)
                {
                    var pathToSave = Path.Combine(downloadsFolder.AbsolutePath, Path.GetFileName(filePath));
                    var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

                    using var fileOutputStream = new FileOutputStream(new File(pathToSave));
                    await fileOutputStream.WriteAsync(bytes);
                    fileOutputStream.Close();

                    isSuccess = true;
                }
            }
        }
        catch
        {
            //
        }

        return isSuccess;
    }

    #endregion
}