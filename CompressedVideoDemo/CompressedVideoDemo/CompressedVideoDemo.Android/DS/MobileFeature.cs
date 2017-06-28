using System;

using Android.App;
using Android.Content;
using Android.Widget;
using CompressedVideoDemo.Interface;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Media;
[assembly: Dependency(typeof(CompressedVideoDemo.Droid.DS.MobileFeature))]

namespace CompressedVideoDemo.Droid.DS
{
    public class MobileFeature:IMobileFeature
    {
        static TaskCompletionSource<string> _tcsVideo;
        static bool isBusy = false;
        static int duration;
        static Activity CurrentActivity
        {
            get
            {
                return (Forms.Context as MainActivity);
            }
        }

        class OnPreparedListener : Java.Lang.Object, MediaPlayer.IOnPreparedListener
        {
            public void OnPrepared(MediaPlayer mp)
            {
                duration = mp.Duration;
                mp.SetVolume(0.0f, 0.0f);
                duration = duration / 1000;
            }
        }

        public static void HandleActivity(int requestCode, Result resultCode, Intent data)
        {
            isBusy = false;
            if (requestCode == IntentHelper.RequestCodes.SelectVideo)
            {
                if (resultCode == Result.Ok)
                {
                    var path = ImageFilePath.GetPath(Forms.Context, data.Data);
                    if (!System.IO.File.Exists(path))
                    {
                        Toast.MakeText(Forms.Context, "Invalid file source", ToastLength.Long).Show();
                        _tcsVideo.SetResult(null);
                        return;
                    }
                    ////get duration
                    //MediaMetadataRetriever retriever = new MediaMetadataRetriever();
                    //retriever.SetDataSource(path);
                    //string time = retriever.ExtractMetadata(MetadataKey.Duration);
                    //long duration = Java.Lang.Long.ParseLong(time);

                    //if (duration / 1000 <= 45)
                    //    _tcsVideo.SetResult(path);
                    //else
                    //{
                    //    //Toast.MakeText(Forms.Context, "Please select video less than 30 seconds", ToastLength.Long).Show();
                    //    _tcsVideo.SetResult(App.VideoDurationCheckMessage);
                    //}
                }
                else
                    _tcsVideo.SetResult(null);
            }
        }

        public Task<string> RecordVideo()
        {
            var task = new TaskCompletionSource<string>();
            try
            {
                IntentHelper.RecordVideo((path) =>
                {
                    task.SetResult(path);
                });
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
            return task.Task;
        }

        public Task<string> SelectVideo()
        {
            var task = new TaskCompletionSource<string>();
            try
            {
                IntentHelper.SelectVideo((path) =>
                {
                    task.SetResult(path);
                });
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
            return task.Task;
           
        }

        public Task<bool> PlayVideo(string path)
        {
            var task = new TaskCompletionSource<bool>();
            try
            {
                IntentHelper.PlayVideo(path, (u) =>
                {
                    task.SetResult(true);
                });
            }
            catch (Exception ex)
            {
                task.SetResult(false);
            }
            return task.Task;
        }

        public Task<bool> CompressVideo(string path,string path2)
        {
            var task = new TaskCompletionSource<bool>();
            try
            {
                IntentHelper.CompressVideo( path,path2, (u) =>
                {
                    task.SetResult(true);
                });
            }
            catch (Exception ex)
            {
                task.SetResult(false);
            }
            return task.Task;
        }
    }
}