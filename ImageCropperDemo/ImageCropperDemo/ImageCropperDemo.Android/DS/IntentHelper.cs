using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
namespace ImageCropperDemo.Droid.DS
{
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