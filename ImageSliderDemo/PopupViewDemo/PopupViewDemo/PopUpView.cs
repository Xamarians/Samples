using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace PopupViewDemo
{
    public class PopUpView:ContentView
    {
        public PopUpView()
        {
            BackgroundColor = Color.FromHex("75000000");
            Padding = 10;

            var PopUp = new StackLayout
            {
                HeightRequest = 170,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = 10,
                Spacing = 0,
            };

            var btn1 = new Button
            {
                Text = "Button1",
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                HeightRequest = 50,
                WidthRequest = 200,
                BorderRadius = 0,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand

            };
            var btn2 = new Button
            {
                Text = "Button2",
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                HeightRequest = 50,
                WidthRequest = 200,
                BorderRadius = 0,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
          

            var header = new Grid()
            {
                HeightRequest = 50,
                BackgroundColor = Color.ForestGreen,
                ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = 80 }
                        }

            };
            header.Children.Add(new Label
            {
                Text = "Choose Options",
                HeightRequest = 50,
                FontSize = 20,
                Margin = new Thickness(5, 0),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start,
                TextColor = Color.Black,
            }, 0, 0);
            PopUp.Children.Add(header);
            PopUp.Children.Add(btn1);
            PopUp.Children.Add(new BoxView() { HeightRequest = 1, BackgroundColor = Color.FromHex("#999999"), HorizontalOptions = LayoutOptions.FillAndExpand });
            PopUp.Children.Add(btn2);
            Content = PopUp;

        }
    }
}

   