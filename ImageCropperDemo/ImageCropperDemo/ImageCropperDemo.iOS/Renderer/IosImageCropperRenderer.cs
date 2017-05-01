using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ImageCropperDemo.Views;
using UIKit;
using CoreGraphics;
[assembly: ExportRenderer(typeof(IosImageCropperPage), typeof(ImageCropperDemo.iOS.Renderer.IosImageCropperRenderer))]

namespace ImageCropperDemo.iOS.Renderer
{
    class IosImageCropperRenderer:PageRenderer
    {
        UIImageView imageView;
        CropperView cropperView;
        UIPanGestureRecognizer pan;
        UIPinchGestureRecognizer pinch;
        string ImagePath;
        IosImageCropperPage element;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            element = e.NewElement as IosImageCropperPage;
            if (element == null)
                return;

            ImagePath = element.ImageUrl;
            var view = NativeView;

            var btnCancel = new UIButton(new CGRect(20, view.Frame.Height - 60, 100, 40));
            btnCancel.SetTitle("Cancel", UIControlState.Normal);
            btnCancel.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnCancel.BackgroundColor = UIColor.Red;
            btnCancel.Layer.CornerRadius = 20;
            btnCancel.TouchUpInside += delegate
            {
                element.OnCancel();
            };

            var btnSave = new UIButton(new CGRect(view.Frame.Width - 120, view.Frame.Height - 60, 100, 40));
            btnSave.SetTitle("Save", UIControlState.Normal);
            btnSave.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnSave.BackgroundColor = UIColor.Green;
            btnSave.Layer.CornerRadius = 20;
            btnSave.TouchUpInside += Crop;

            view.Add(btnCancel);
            view.Add(btnSave);
        }

        double imageWidth, imageHeight;
        double xRatio, yRatio;
        public override void ViewDidLoad()
        {
            double maxWidth = NativeView.Frame.Width; // Platforms.DeviceHelper.DeviceWidth;
            double maxHeight = NativeView.Frame.Height;  //Platforms.DeviceHelper.DeviceHeight;
            base.ViewDidLoad();
            if (ImagePath == null)
                return;
            using (var image = UIImage.FromFile(ImagePath))
            {

                imageWidth = Math.Min(image.Size.Width, maxWidth);
                imageHeight = imageWidth * image.Size.Height / image.Size.Width;
                xRatio = image.Size.Width / imageWidth;
                yRatio = image.Size.Height / imageHeight;

                imageView = new UIImageView(new CGRect(0, 20, imageWidth, imageHeight));
                imageView.Image = image;

            }
            double cropW = Math.Min(imageWidth, imageHeight) > 200 ? 200 : 150;
            cropperView = new CropperView(0, 0, cropW, cropW) { Frame = imageView.Frame };
            View.AddSubviews(imageView, cropperView);

            nfloat dx = 0;
            nfloat dy = 0;

            pan = new UIPanGestureRecognizer(() =>
            {
                if ((pan.State == UIGestureRecognizerState.Began || pan.State == UIGestureRecognizerState.Changed) && (pan.NumberOfTouches == 1))
                {

                    var p0 = pan.LocationInView(View);

                    if (dx == 0)
                        dx = p0.X - cropperView.Origin.X;

                    if (dy == 0)
                        dy = p0.Y - cropperView.Origin.Y;

                    var p1 = new CGPoint(p0.X - dx, p0.Y - dy);

                    cropperView.Origin = p1;
                }
                else if (pan.State == UIGestureRecognizerState.Ended)
                {
                    dx = 0;
                    dy = 0;
                }
            });

            nfloat s0 = 1;

            pinch = new UIPinchGestureRecognizer(() =>
            {
                nfloat s = pinch.Scale;
                nfloat ds = (nfloat)Math.Abs(s - s0);
                nfloat sf = 0;
                const float rate = 0.5f;

                if (s >= s0)
                {
                    sf = 1 + ds * rate;
                }
                else if (s < s0)
                {
                    sf = 1 - ds * rate;
                }
                s0 = s;

                cropperView.CropSize = new CGSize(cropperView.CropSize.Width * sf, cropperView.CropSize.Height * sf);

                if (pinch.State == UIGestureRecognizerState.Ended)
                {
                    s0 = 1;
                }
            });

            cropperView.AddGestureRecognizer(pan);
            cropperView.AddGestureRecognizer(pinch);
        }

        private void Crop(object sender, EventArgs e)
        {
            var rect = cropperView.GetCropRect(1, 1);
            var uiImage = UIImage.FromFile(ImagePath).Scale(new CGSize(imageWidth, imageHeight));

            bool orientationChange = false;
            if (uiImage.Orientation == UIImageOrientation.Right)
            {
                orientationChange = true;
                var w = imageWidth;//* xRatio;
                var h = imageHeight;//* yRatio;
                rect = new CGRect(rect.Y, w - rect.X - rect.Width, rect.Width, rect.Height);
            }

            var image = uiImage.CGImage.WithImageInRect(rect);

            if (orientationChange)
            {
                using (var rotateImage = new UIImage(image, scale: 1, orientation: UIImageOrientation.Right))
                {
                    rotateImage.AsJPEG().Save(element.DestinationPath, false);
                    element.OnSave();
                }
            }
            else
            {
                using (var croppedImage = UIImage.FromImage(image))
                {
                    croppedImage.AsJPEG().Save(element.DestinationPath, false);
                    element.OnSave();
                }
            }
        }
    }
}