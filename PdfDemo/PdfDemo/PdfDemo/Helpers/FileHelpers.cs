using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfDemo.Helpers
{
  static  class FileHelpers
    {

#if __ANDROID__
        public static readonly string DirectoryPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments) + "/PdfSample";
#endif
#if __IOS__
		public static readonly string DirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/PdfSample";
#endif

        static FileHelpers()
        {
            CheckAndCreateAppDirectory();
        }

        public static void CheckAndCreateAppDirectory()
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);
        }
    }
}
