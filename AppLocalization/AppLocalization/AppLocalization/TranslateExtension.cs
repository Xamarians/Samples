﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.Resources;
using AppLocalization.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppLocalization
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci;
        string ResourceId = Device.OnPlatform("AppLocalization.iOS.Resx.AppResource", "AppLocalization.Droid.Resx.AppResource", "AppLocalization.WinPhone.Resx.AppResource");

        public TranslateExtension()
        {
            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                 ci = DependencyService.Get<ILocalise>().GetCurrentCultureInfo();
                //ci = AppLocalization.Resx.AppResource.Culture;
            }
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager resmgr = new ResourceManager(ResourceId
                                , typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = resmgr.GetString(Text, ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, ci.Name),
                    "Text");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}
