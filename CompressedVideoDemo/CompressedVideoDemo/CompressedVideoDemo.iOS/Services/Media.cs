using Foundation;
using MobileCoreServices;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace CompressedVideoDemo.iOS.Services
{
  public static class Media
    {
        static UIImagePickerController picker;

        static Action<NSDictionary> _callback;

        static void Init()
        {
            if (picker != null)
                return;

            picker = new UIImagePickerController();
            picker.Delegate = new MediaDelegate();
        }

        class MediaDelegate : UIImagePickerControllerDelegate
        {
            public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
            {
                var cb = _callback;
                _callback = null;
                picker.DismissModalViewController(true);
                cb(info);
            }

            public override void Canceled(UIImagePickerController picker)
            {
                var cb = _callback;
                _callback = null;
                picker.DismissModalViewController(true);
                cb(null);
            }
        }
        public static void RecordVideo(UIViewController parent, Action<NSDictionary> callback)
        {
            if (!UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front | UIImagePickerControllerCameraDevice.Rear))
            {
                callback(null);
                return;
            }
            Init();
            picker.SourceType = UIImagePickerControllerSourceType.Camera;
            picker.VideoQuality = UIImagePickerControllerQualityType.Medium;

            picker.MediaTypes = new string[] { UTType.Movie };
            _callback = callback;
            parent.PresentModalViewController(picker, true);
        }

    }
}
