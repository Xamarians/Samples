using Android.Runtime;
using System;
using Xamarin.Facebook;

namespace SocialLogin.Droid.Platforms
{
    class FacebookCallback<TResult> : Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
    {
        public Action HandleCancel { get; set; }
        public Action<FacebookException> HandleError { get; set; }
        public Action<TResult> HandleSuccess { get; set; }

        public void OnCancel()
        {
            if (HandleCancel != null) HandleCancel();
        }

        public void OnError(FacebookException error)
        {
            if (HandleError != null) HandleError(error);
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            if (HandleSuccess != null) HandleSuccess(result.JavaCast<TResult>());
        }
    }

}