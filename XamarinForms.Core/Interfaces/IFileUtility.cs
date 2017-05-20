namespace XamarinForms.Core.Interfaces
{
    public interface IFileUtility
    {
        string SaveFile(string filename, string foldername, byte[] fileStream);
        byte[] LoadFile(string filename, string folderName);
        void DeleteFile(string filename, string foldername);
        void DeleteDirectory(string foldername);
    }

    //Droid
    //public class FileUtility : IFileUtility
    //{
    //    public string SaveFile(string fileName, byte[] imageStream)
    //    {
    //        string path = null;
    //        string imageFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ProductImages");

    //        //Check if the folder exist or not
    //        if (!System.IO.Directory.Exists(imageFolderPath))
    //        {
    //            System.IO.Directory.CreateDirectory(imageFolderPath);
    //        }
    //        string imagefilePath = System.IO.Path.Combine(imageFolderPath, fileName);

    //        //Try to write the file bytes to the specified location.
    //        try
    //        {
    //            System.IO.File.WriteAllBytes(imagefilePath, imageStream);
    //            path = imagefilePath;
    //        }
    //        catch (System.Exception e)
    //        {
    //            throw e;
    //        }
    //        return path;
    //    }

    //    public void DeleteDirectory()
    //    {
    //        string imageFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ProductImages");
    //        if (System.IO.Directory.Exists(imageFolderPath))
    //        {
    //            System.IO.Directory.Delete(imageFolderPath, true);
    //        }
    //    }
    //}
    //____________________________________________________
    //ios
    //public class FileUtility : IFileUtility
    //{
    //    public string SaveFile(string fileName, byte[] fileStream)
    //    {
    //        string path = null;
    //        string imageFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ProductImages");

    //        //Check if the folder exist or not
    //        if (!System.IO.Directory.Exists(imageFolderPath))
    //        {
    //            System.IO.Directory.CreateDirectory(imageFolderPath);
    //        }
    //        string imagefilePath = System.IO.Path.Combine(imageFolderPath, fileName);

    //        //Try to write the file bytes to the specified location.
    //        try
    //        {
    //            System.IO.File.WriteAllBytes(imagefilePath, fileStream);
    //            path = imagefilePath;
    //        }
    //        catch (System.Exception e)
    //        {
    //            throw e;
    //        }
    //        return path;
    //    }

    //    public void DeleteDirectory()
    //    {
    //        string imageFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ProductImages");
    //        if (System.IO.Directory.Exists(imageFolderPath))
    //        {
    //            System.IO.Directory.Delete(imageFolderPath, true);
    //        }
    //    }
    //}
}
