using Android.Content;
using Android.Database;
using Android.Net;
using Android.OS;
using Android.Provider;
namespace CompressedVideoDemo.Droid.DS
{
   public class ImageFilePath
    { 
    public static string GetPath(Context context, Uri uri)
    {

        //check here to KITKAT or new version
        bool isKitKat = Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat;

        // DocumentProvider
        if (isKitKat && DocumentsContract.IsDocumentUri(context, uri))
        {

            // ExternalStorageProvider
            if (IsExternalStorageDocument(uri))
            {
                string docId = DocumentsContract.GetDocumentId(uri);
                string[] split = docId.Split(':');
                string type = split[0];

                if ("primary".Equals(type, System.StringComparison.OrdinalIgnoreCase))
                {
                    return Environment.ExternalStorageDirectory + "/" + split[1];
                }
            }
            // DownloadsProvider
            else if (IsDownloadsDocument(uri))
            {

                string id = DocumentsContract.GetDocumentId(uri);
                Uri contentUri = ContentUris.WithAppendedId(Uri.Parse("content://downloads/public_downloads"), long.Parse(id));
                return GetDataColumn(context, contentUri, null, null);
            }
            // MediaProvider
            else if (IsMediaDocument(uri))
            {
                string docId = DocumentsContract.GetDocumentId(uri);
                string[] split = docId.Split(':');
                string type = split[0];

                Uri contentUri = null;
                if ("image".Equals(type))
                {
                    contentUri = MediaStore.Images.Media.ExternalContentUri;
                }
                else if ("video".Equals(type))
                {
                    contentUri = MediaStore.Video.Media.ExternalContentUri;
                }
                else if ("audio".Equals(type))
                {
                    contentUri = MediaStore.Audio.Media.ExternalContentUri;
                }

                string selection = "_id=?";
                string[] selectionArgs = new string[] { split[1] };

                return GetDataColumn(context, contentUri, selection, selectionArgs);
            }
        }
        // MediaStore (and general)
        else if ("content".Equals(uri.Scheme, System.StringComparison.OrdinalIgnoreCase))
        {

            // Return the remote address
            if (IsGooglePhotosUri(uri))
                return uri.LastPathSegment;

            return GetDataColumn(context, uri, null, null);
        }
        // File
        else if ("file".Equals(uri.Scheme, System.StringComparison.OrdinalIgnoreCase))
        {
            return uri.Path;
        }

        return null;
    }

    /**
     * Get the value of the data column for this Uri. This is useful for
     * MediaStore Uris, and other file-based ContentProviders.
     *
     * @param context The context.
     * @param uri The Uri to query.
     * @param selection (Optional) Filter used in the query.
     * @param selectionArgs (Optional) Selection arguments used in the query.
     * @return The value of the _data column, which is typically a file path.
     */
    private static string GetDataColumn(Context context, Uri uri, string selection,
      string[] selectionArgs)
    {
        ICursor cursor = null;
        string column = "_data";
        string[] projection = {
                column
            };

        try
        {
            cursor = context.ContentResolver.Query(uri, projection, selection, selectionArgs,
              null);
            if (cursor != null && cursor.MoveToFirst())
            {
                int index = cursor.GetColumnIndexOrThrow(column);
                return cursor.GetString(index);
            }
        }
        finally
        {
            if (cursor != null)
                cursor.Close();
        }
        return null;
    }

    /**
     * @param uri The Uri to check.
     * @return Whether the Uri authority is ExternalStorageProvider.
     */
    public static bool IsExternalStorageDocument(Uri uri)
    {
        return "com.android.externalstorage.documents".Equals(uri.Authority);
    }

    /**
     * @param uri The Uri to check.
     * @return Whether the Uri authority is DownloadsProvider.
     */
    public static bool IsDownloadsDocument(Uri uri)
    {
        return "com.android.providers.downloads.documents".Equals(uri.Authority);
    }

    /**
     * @param uri The Uri to check.
     * @return Whether the Uri authority is MediaProvider.
     */
    public static bool IsMediaDocument(Uri uri)
    {
        return "com.android.providers.media.documents".Equals(uri.Authority);
    }

    /**
     * @param uri The Uri to check.
     * @return Whether the Uri authority is Google Photos.
     */
    public static bool IsGooglePhotosUri(Uri uri)
    {
        return "com.google.android.apps.photos.content".Equals(uri.Authority);
    }
}
}
