using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageCropperDemo.Helpers
{
    static class FileHelper
    {

#if __ANDROID__
        public static readonly string PictureDirectoryPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures) + "/CroppedImage";
#endif
#if __IOS__
        public static readonly string PictureDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/CroppedImage";
#endif

        static FileHelper()
        {
            CheckAndCreateAppDirectory();
        }

        public static void CheckAndCreateAppDirectory()
        {
            if (!Directory.Exists(PictureDirectoryPath))
                Directory.CreateDirectory(PictureDirectoryPath);
        }

        public static string MapPicturePath(string fileName)
        {
            return Path.Combine(PictureDirectoryPath, fileName);
        }

        public static void CopyImageToPath(string sourceFilePath, string targetFilePath)
        {
            File.Copy(sourceFilePath, targetFilePath);
        }

       
        public static string GenerateUniqueFileName(string ext)
        {
            if (!ext.StartsWith("."))
                ext = "." + ext;
            return string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddhhmmssfff"), ext);
        }

        public static string GenerateUniqueFilePath(string ext)
        {
            if (!ext.StartsWith("."))
                ext = "." + ext;
            return MapPicturePath(string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddhhmmssfff"), ext));
        }


        public static string GetExtension(string filename, string defaultExt)
        {
            return filename == null ? defaultExt : filename.Split('.').LastOrDefault() ?? defaultExt;
        }

    }
}
