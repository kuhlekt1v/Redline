using System;

using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class ManageAccountPage : ContentPage
    {
        public ManageAccountPage()
        {

            StackLayout outerStack = new StackLayout
            {
                BackgroundColor = (Color)Application.Current.Resources["AccentColor"],
                Children =
                {
                    // Page title label.
                    new Label 
                    { 
                        Text = "Account Settings", 
                        Padding = new Thickness(10,20, 10, 20), 
                        Style = (Style)Application.Current.Resources["HeaderTitleStyle"] 
                    }
                }
            };

            // Main content area.
            Frame mainContentArea = new Frame
            {
                CornerRadius = 13,
                Padding = 0,
                Margin = new Thickness(0, 0, 0, -10),
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = (Color)Application.Current.Resources["BackgroundColor"]
            };

            // Compose the view.
            outerStack.Children.Add(mainContentArea);

            Content = outerStack;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            // SAMPLE

            //Content = new StackLayout
            //{
            //    Margin = new Thickness(20),
            //    Children =
            //    {
            //        new Label { Text = "Start", BackgroundColor = Color.Gray, HorizontalOptions = LayoutOptions.Start },
            //        new Label { Text = "Center", BackgroundColor = Color.Gray, HorizontalOptions = LayoutOptions.Center },
            //        new Label { Text = "End", BackgroundColor = Color.Gray, HorizontalOptions = LayoutOptions.End },
            //        new Label { Text = "Fill", BackgroundColor = Color.Gray, HorizontalOptions = LayoutOptions.Fill }
            //    }
            //};
        }
    }



    //private void Button_Clicked(object sender, EventArgs e)
    //    {
    //        //var defaultCategories = DefaultCategoryStack.Children;
    //        //DefaultCategoryStack.Children.Clear();
    //    }

    //    private void Button_Clicked_1(object sender, EventArgs e)
    //    {

    //    }
    //}
}