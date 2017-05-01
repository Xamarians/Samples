using System;
using Foundation;
using UIKit;
using MobileCoreServices;

namespace ImageCropperDemo.iOS.Services
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

    public static void TakePicture(UIViewController parent, Action<NSDictionary> callback)
    {

        if (!UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front | UIImagePickerControllerCameraDevice.Rear))
        {
            callback(null);
            return;
        }

        Init();
        picker.SourceType = UIImagePickerControllerSourceType.Camera;
        picker.MediaTypes = new string[] { UTType.Image };
        _callback = callback;
        parent.PresentModalViewController(picker, true);

    }

    public static void SelectPicture(UIViewController parent, Action<NSDictionary> callback)
    {
        Init();
        picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
        _callback = callback;
        parent.PresentModalViewController(picker, true);
    }
}
}
