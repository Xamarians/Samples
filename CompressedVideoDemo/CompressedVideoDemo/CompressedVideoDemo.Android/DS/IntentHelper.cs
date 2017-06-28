using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Java.IO;
using CompressedVideoDemo.Helpers;
using System.Windows.Input;
using XamarinAndroidFFmpeg;
using Xamarin.Forms;

namespace CompressedVideoDemo.Droid.DS
{
    internal class IntentHelper
    {
        static readonly Dictionary<int, Action<Result, Intent>> _CallbackDictionary = new Dictionary<int, Action<Result, Intent>>();
        public struct RequestCodes
        {

            public const int RecordVideo = 103;
            public const int PlayVideo = 104;
            public const int CompressVideo = 106;
            public const int SelectVideo = 105;

        }

        static Java.IO.File _destFile;

        static Action<string> _callback;

        static Activity CurrentActivity
        {
            get
            {
                return (Xamarin.Forms.Forms.Context as MainActivity);
            }
        }

        #region Public Methods

        public static void StartIntent(Intent intent, int requestCode, Action<Result, Intent> callback)
        {
            if (_CallbackDictionary.ContainsKey(requestCode))
                _CallbackDictionary.Remove(requestCode);
            _CallbackDictionary.Add(requestCode, callback);
            (Xamarin.Forms.Forms.Context as Activity).StartActivityForResult(intent, requestCode);
        }


        public static void ActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (_callback == null)
                return;
            if (resultCode == Result.Ok)
            {
                if (requestCode == RequestCodes.RecordVideo)
                {
                    _callback(_destFile.Path);
                }

                else if (requestCode == RequestCodes.CompressVideo)
                {
                }
                else if (requestCode == RequestCodes.SelectVideo)
                {
                    var destFilePath = ImageFilePath.GetPath(CurrentActivity, data.Data);
                    _callback(destFilePath);
                }

                else if (requestCode == RequestCodes.PlayVideo)
                {
                    _callback(null);
                }
            }
        }

        public static bool IsMobileIntent(int code)
        {
            return code == (int)RequestCodes.RecordVideo
                || code == (int)RequestCodes.PlayVideo
                || code == (int)RequestCodes.CompressVideo
                || code == (int)RequestCodes.SelectVideo;
        }



        public static void RecordVideo(Action<string> callback)
        {
            _callback = callback;
            _destFile = new File(FileHelper.CreateNewVideoPath());
            Intent captureVideoIntent = new Intent(MediaStore.ActionVideoCapture);
            captureVideoIntent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_destFile));
            //   captureVideoIntent.PutExtra(MediaStore.ExtraVideoQuality, 1);
            CurrentActivity.StartActivityForResult(captureVideoIntent, RequestCodes.RecordVideo);
        }

        public static void SelectVideo(Action<string> callback)
        {
            _callback = callback;
            _destFile = new File(FileHelper.CreateNewVideoPath());
            Intent selectVideoIntent = new Intent(Intent.ActionPick);
            selectVideoIntent.SetType("video/*");
            selectVideoIntent.SetAction(Intent.ActionGetContent);
            CurrentActivity.StartActivityForResult(Intent.CreateChooser(selectVideoIntent, "Select Video"), RequestCodes.SelectVideo);
        }
        public static void CompressVideo(string inputPath, string outputPath, Action<string> callback)
        {
            Activity activity = new Activity();
            _callback = callback;
            ProgressDialog progress = new ProgressDialog(Forms.Context);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Compressing Video. Please wait...");
            progress.SetCancelable(false);
            progress.Show();

            Task.Run(() =>
            {
                var _workingDirectory = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
                var sourceMp4 = inputPath;
                var destinationPath1 = outputPath;
                FFMpeg ffmpeg = new FFMpeg(Android.App.Application.Context, _workingDirectory);
                TransposeVideoFilter vfTranspose = new TransposeVideoFilter(TransposeVideoFilter.NINETY_CLOCKWISE);
                var filters = new List<VideoFilter>();
                filters.Add(vfTranspose);

                var sourceClip = new Clip(System.IO.Path.Combine(_workingDirectory, sourceMp4)) { videoFilter = VideoFilter.Build(filters) };
                var br = System.Environment.NewLine;
                var onComplete = new MyCommand((_) =>
                {
                        _callback(destinationPath1);
                        progress.Dismiss();
                });

                var onMessage = new MyCommand((message) =>
                {
                    System.Console.WriteLine(message);
                });

                var callbacks = new FFMpegCallbacks(onComplete, onMessage);
                string[] cmds = new string[] {
                "-y",
                "-i",
                sourceClip.path,
               "-strict", "experimental",
                        "-vcodec", "libx264",
                        "-preset", "ultrafast",
                        "-crf","30", "-acodec","aac", "-ar", "44100" ,
                        "-q:v", "20",
                  "-vf",sourceClip.videoFilter,
                 // "mp=eq2=1:1.68:0.3:1.25:1:0.96:1",

                destinationPath1 ,
            };
                ffmpeg.Execute(cmds, callbacks);
            });
        }

        public class MyCommand : ICommand
        {
            public delegate void ICommandOnExecute(object parameter = null);
            public delegate bool ICommandOnCanExecute(object parameter);

            private ICommandOnExecute _execute;
            private ICommandOnCanExecute _canExecute;

            public MyCommand(ICommandOnExecute onExecuteMethod)
            {
                _execute = onExecuteMethod;
            }

            public MyCommand(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute onCanExecuteMethod)
            {
                _execute = onExecuteMethod;
                _canExecute = onCanExecuteMethod;
            }

            #region ICommand Members

            public event EventHandler CanExecuteChanged
            {
                add { throw new NotImplementedException(); }
                remove { throw new NotImplementedException(); }
            }

            public bool CanExecute(object parameter)
            {
                if (_canExecute == null && _execute != null)
                    return true;

                return _canExecute.Invoke(parameter);
            }

            public void Execute(object parameter)
            {
                if (_execute == null)
                    return;

                _execute.Invoke(parameter);
            }

            #endregion
        }



        public static void PlayVideo(string videoPath, Action<string> callback)
        {
            _callback = callback;
            try
            {
                Intent playVideoIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(videoPath));
                playVideoIntent.SetDataAndType(Android.Net.Uri.Parse(videoPath), "video/mp4");
                CurrentActivity.StartActivityForResult(playVideoIntent, RequestCodes.PlayVideo);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion


    }

    static class IntentHelpers
    {
        static readonly Dictionary<int, Action<Result, Intent>> _CallbackDictionary = new Dictionary<int, Action<Result, Intent>>();

        internal static void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (!_CallbackDictionary.ContainsKey(requestCode))
                return;
            _CallbackDictionary[requestCode].Invoke(resultCode, data);
            _CallbackDictionary.Remove(requestCode);
        }

        public static void StartIntent(Intent intent, int requestCode, Action<Result, Intent> callback)
        {
            if (_CallbackDictionary.ContainsKey(requestCode))
                _CallbackDictionary.Remove(requestCode);
            _CallbackDictionary.Add(requestCode, callback);
            (Xamarin.Forms.Forms.Context as Activity).StartActivityForResult(intent, requestCode);
        }
    }

}