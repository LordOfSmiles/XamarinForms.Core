using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Provider;
using Java.IO;
using Xamarin.Essentials;
using XamarinForms.Core.Helpers;
using XamarinForms.Core.PlatformServices;
using Environment = Android.OS.Environment;
using File = Java.IO.File;

namespace XamarinForms.Droid.PlatformServices;

public sealed class DroidFileSaver : IDroidFileSaver
{
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
        
        return isSuccess;
    }

    private static async Task<bool> OldImplementationAsync(string filePath)
    {
        var isSuccess = false;
        
        try
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
        catch
        {
            //
        }

        return isSuccess;
    }
    
    #endregion
}