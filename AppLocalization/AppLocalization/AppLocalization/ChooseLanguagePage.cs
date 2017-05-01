using AppLocalization.Resx;
using System;
using System.Collections.Generic;
using AppLocalization.Interface;
using System.Text;
using Xamarin.Forms;

namespace AppLocalization
{
    public class ChooseLanguagePage : ContentPage
    {
        public ChooseLanguagePage()
        {
            var btnEnglish = new Button() { Text = "English", BackgroundColor = Color.Aqua, VerticalOptions = LayoutOptions.CenterAndExpand };
            btnEnglish.Clicked += BtnEnglish_Clicked;
            var btnChinese = new Button() { Text = "Chinese", BackgroundColor = Color.Aqua, VerticalOptions = LayoutOptions.CenterAndExpand };
            btnChinese.Clicked += BtnChinese_Clicked;
            Content = new StackLayout
            {
                Padding = new Thickness(20),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Select Language",
                            FontAttributes = FontAttributes.Bold
                        },
                        btnEnglish,
                        btnChinese,
                    }
            };
        }

        private void BtnChinese_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<ILocalise>().SetLocale(new System.Globalization.CultureInfo("zh-Hant"));
            Navigation.PushAsync(new AppLocalisationXaml());
        }

        private void BtnEnglish_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<ILocalise>().SetLocale(new System.Globalization.CultureInfo("en-US"));
            Navigation.PushAsync(new AppLocalisationXaml());
        }
    }
}
