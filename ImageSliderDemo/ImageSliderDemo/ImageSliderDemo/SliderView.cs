using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
namespace ImageSliderDemo
{
    public class SliderView : ContentView
    {
        View _currentView;
        double _height, _width;
        int currentViewIndex = 0;
        Button button1, button2;

        public SliderView(View rootview, double height, double width)
        {
            _height = height;
            _width = width;

            //Set the current view to the view that was initialized
            _currentView = rootview;
            BackgroundColor = Color.Gray;

            //Create the ObservableCollection for the Children property
            Children = new ObservableCollection<View>();
            Children.Insert(0, _currentView);

            //Create the ViewScreen that will schol the current layout.
            ViewScreen = new AbsoluteLayout
            {
                HeightRequest = _height,
                WidthRequest = _width,
            };

            button1 = new Button
            {
                Image = "back.png",
                BackgroundColor = Color.Transparent,

            };
            button2 = new Button
            {
                Image = "right_arrow.png",
                BackgroundColor = Color.Transparent,
            };
            button1.Clicked += (object sender, EventArgs e) =>
            {
                OnLeftButtonClicked();
            };



            button2.Clicked += (object sender, EventArgs e) =>
            {
                OnRightButtonClicked();
            };

            //Add ObservableCollection CollectionChanged event to update the little white dots as children are added and removed
            Children.CollectionChanged += Children_CollectionChanged;

            Rectangle dotRect = new Rectangle(
                x: _width - 60,
                y: _height / 2 - (20),
                width: 50,
                height: 40
            );

            //Layout the current view to the ViewScreen
            ViewScreen.Children.Add(_currentView, new Rectangle(0, 0, width, height));
            ViewScreen.Children.Add(button1, new Rectangle(0, .5, 50, 50), AbsoluteLayoutFlags.PositionProportional);
            ViewScreen.Children.Add(button2, new Rectangle(1, .5, 50, 50), AbsoluteLayoutFlags.PositionProportional);

            //Set the content of the ContentView to the ViewScreen	
            Content = ViewScreen;
        }



        void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateArrow();
        }

        public int MinimumSwipeDistance
        {
            get;
            set;
        }

        public AbsoluteLayout ViewScreen
        {
            get;
            set;
        }

        public ObservableCollection<View> Children
        {
            get;
            set;
        }
        public View CurrentView
        {
            get; set;
        }

        public uint TransitionLength
        {
            get;
            set;
        }


        public void OnLeftButtonClicked()
        {
            Rectangle initialLayoutRect = new Rectangle(
              0,
              0,
              _width,
              _height
          );
            if (currentViewIndex != 0)
            {
                //Drop the index one
                currentViewIndex--;
                _currentView = Children[currentViewIndex];

                initialLayoutRect.X = -this.ParentView.Width;

                ViewScreen.Children.Add(_currentView, initialLayoutRect);
                //Translate the currentview into ViewScreen. CurrentView is now on screen
                var abc = _currentView.TranslateTo(ParentView.Width, 0, TransitionLength);

                if (Children[currentViewIndex + 1] is Layout)
                {
                    //This viewgroup contains the ViewScreen that we need to get at child 0
                    var view = _currentView;
                }
                //Remove the old view from the back of the ViewScreen
                ViewScreen.Children.Remove(Children[currentViewIndex + 1]);
            }
            UpdateArrow();
        }

        public void OnRightButtonClicked()
        {
            if (Children.Count > currentViewIndex + 1)
            {
                currentViewIndex++;
                _currentView = Children[currentViewIndex];

                Rectangle initialLayoutRect = new Rectangle(
                  0,
                  0,
                  _width,
                  _height
              );
                initialLayoutRect.X = this.ParentView.Width;

                //Add the CurrentView to the ViewScreen, but it is still off screen
                ViewScreen.Children.Add(_currentView, initialLayoutRect);

                //Translate the currentview into ViewScreen. CurrentView is now on screen
                var abc = _currentView.TranslateTo(-ParentView.Width, 0, TransitionLength);
                if (Children[currentViewIndex - 1] is Layout)
                {
                    //This viewgroup contains the ViewScreen that we need to get at child 0
                    var view = _currentView;
                }
                //Remove the old view from the back of the ViewScreen
                ViewScreen.Children.Remove(Children[currentViewIndex - 1]);
            }
            UpdateArrow();
        }

        public void UpdateArrow()
        {
            ViewScreen.Children.Remove(button1);
            ViewScreen.Children.Remove(button2);
            ViewScreen.Children.Add(button1, new Rectangle(0, .5, 50, 40), AbsoluteLayoutFlags.PositionProportional);
            ViewScreen.Children.Add(button2, new Rectangle(1, .5, 50, 40), AbsoluteLayoutFlags.PositionProportional);
        }
    }
}
