using System;
using System.Collections.Generic;
using System.Text;
using MediaPlayer;
using Foundation;
using AVFoundation;
using System.Threading.Tasks;
using CompressedVideoDemo.iOS.DS;
using CompressedVideoDemo.Interface;
using UIKit;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(MobileFeature))]

namespace CompressedVideoDemo.iOS.DS
{
    class MobileFeature : IMobileFeature
    {
        readonly UIImagePickerController picker;
        private static UIViewController GetController()
        {
            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;
            return vc;
        }
        public MobileFeature()
        {
            picker = new UIImagePickerController();
            picker.Delegate = new MediaDelegate();
        }
        #region Picker
        static Action<NSDictionary> resultCallback;
        private void ShowPicker(UIImagePickerControllerSourceType type, Action<NSDictionary> callback)
        {
            resultCallback = callback;
            picker.SourceType = type;
            GetController().PresentModalViewController(picker, true);
        }
        class MediaDelegate : UIImagePickerControllerDelegate
        {
            public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
            {
                if (resultCallback == null)
                    return;
                var cb = resultCallback;
                resultCallback = null;
                picker.DismissModalViewController(true);
                cb(info);

            }
            public override void Canceled(UIImagePickerController picker)
            {
                if (resultCallback == null)
                    return;
                var cb = resultCallback;
                resultCallback = null;
                picker.DismissModalViewController(true);
                cb(null);
            }
        }
        #endregion
        TaskCompletionSource<string> tcs;
        public Task<string> SelectVideo()
        {
            tcs = new TaskCompletionSource<string>();
            picker.MediaTypes = new string[] { "public.movie" };
            ShowPicker(UIImagePickerControllerSourceType.PhotoLibrary, (data) =>
            {
                var mediaUrl = data.ValueForKey(new NSString("UIImagePickerControllerMediaURL")) as NSUrl;
                var videoData = NSData.FromUrl(NSUrl.FromString(mediaUrl.ToString()));
                string videoFilename = System.IO.Path.Combine(GetVideoDirectoryPath(), GetUniqueFileName(".mp4"));
                NSError err = null;
                if (videoData.Save(videoFilename, false, out err))
                {
                    tcs.SetResult(videoFilename);
                }
                else
                {
                    tcs.SetResult(null);
                }
            });
            return tcs.Task;
        }

        void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
            if (mediaURL != null)
            {
                tcs.SetResult(mediaURL.Path);

            }
            picker.DismissModalViewController(true);
        }

        #region Private Methods


        private static string GetUniqueFileName(string ext)
        {
            return System.Guid.NewGuid().ToString().Replace("-", "") + ext;
        }
        private string GetVideoDirectoryPath()
        {

            var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Compressed/Video";
            if (!System.IO.Directory.Exists(documentsDirectory))
            {
                System.IO.Directory.CreateDirectory(documentsDirectory);
            }
            return documentsDirectory;
        }
        #endregion
      
        public Task<string> PlayVideo(string videoPath)
        {
            var task = new TaskCompletionSource<string>();
            try
            {
                if (videoPath != null)
                {
                    var moviePlayer = new MPMoviePlayerController(new NSUrl(videoPath));
                    GetController().View.AddSubview(moviePlayer.View);
                    moviePlayer.SetFullscreen(true, true);
                    moviePlayer.Play();
                }
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
            return task.Task;
        }

        public Task<string> RecordVideo()
        {
            var task = new TaskCompletionSource<string>();
            try
            {
                Services.Media.RecordVideo(GetController(), (data) =>
                {
                    if (data == null)
                    {
                        task.SetResult(null);
                        return;
                    }

                    //check for camera
                    var mediaUrl = data.ValueForKey(new NSString("UIImagePickerControllerMediaURL")) as NSUrl;
                    var videoData = NSData.FromUrl(NSUrl.FromString(mediaUrl.ToString()));
                    string videoFilename = Path.Combine(GetVideoDirectoryPath(), GetUniqueFileName(".mp4"));
                    NSError err = null;
                    if (videoData.Save(videoFilename, false, out err))
                    {
                        task.SetResult(videoFilename);
                    }
                    else
                    {
                        task.SetResult(null);
                    }
                });
            }
            catch (Exception ex)
            {
                task.SetException(ex);
            }
            return task.Task;
        }

        public Task<bool> CompressVideo(string inputPath, string outputPath)
        {          
            var task = new TaskCompletionSource<bool>();
            NSString urlString = new NSString(inputPath);
            NSUrl myFileUrl = new NSUrl(urlString);
            var export = new AVAssetExportSession(AVAsset.FromUrl(myFileUrl), AVAssetExportSession.PresetMediumQuality);
            string videoFilename = outputPath;
            export.OutputUrl = NSUrl.FromFilename(videoFilename);
            export.OutputFileType = AVFileType.Mpeg4;
            export.ShouldOptimizeForNetworkUse = true;

            export.ExportAsynchronously(() =>
            {
                if (export.Status == AVAssetExportSessionStatus.Completed)
                {
                    var videoData = NSData.FromUrl(NSUrl.FromString(export.OutputUrl.ToString()));
                    NSError err = null;
                    if (videoData.Save(videoFilename, false, out err))
                    {
                        task.SetResult(true);
                       
                    }
                    else
                    {
                        task.SetResult(true);
                    }
                }
                else
                    task.SetResult(false);
            });

            return task.Task;
        }
    }
}


