using PopupViewDemo.Interface;
using Xamarin.Forms;

namespace PopupViewDemo
{
    public partial class MainPage : ContentPage
    {
        PopUpView view = new PopUpView();
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
             DependencyService.Get<IView>().ShowPopup(view);    
        }
    }

    public class PopUpView : ContentView
    {
        public PopUpView()
        {
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            BackgroundColor = Color.Red;
            
           var PopUp = new StackLayout
            {
                HeightRequest = 170,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = 10,
                Spacing = 0,
                Margin=10
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



            var label = new Label
            {
                Text = "Choose Options",
                HeightRequest = 50,
                WidthRequest=200,
                FontSize = 20,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black,
                BackgroundColor=Color.Green
            };
            PopUp.Children.Add(label);
            PopUp.Children.Add(btn1);
            PopUp.Children.Add(new BoxView() { HeightRequest = 1, BackgroundColor = Color.FromHex("#999999"), HorizontalOptions = LayoutOptions.FillAndExpand });
            PopUp.Children.Add(btn2);
            Content = PopUp;

        }
    }
}

   

