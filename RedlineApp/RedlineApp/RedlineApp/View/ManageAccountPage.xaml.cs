using System;

using Xamarin.Forms;

namespace RedlineApp.View
{
    // REFERENCE: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/button
    public partial class ManageAccountPage : ContentPage
    {
        StackLayout outerStack;

        public ManageAccountPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            outerStack = new StackLayout
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
                BackgroundColor = (Color)Application.Current.Resources["BackgroundColor"],
            };

            Button hideDefaultBtn = new Button
            {
                Text = "Hide TEST"
                // Define properties here
            };

            // Need to add ManageAccountViewModel
            hideDefaultBtn.Clicked += HideDefaultCategories;

            StackLayout defaultCategories = new StackLayout
            {
                Children =
                {
                    hideDefaultBtn
                }
            };



            outerStack.Children.Add(mainContentArea);

            // Compose the view.
            outerStack.Children.Add(mainContentArea);
            outerStack.Children.Add(defaultCategories);

            Content = outerStack;
        }

        void HideDefaultCategories(object sender, EventArgs e)
        {
            outerStack.Children.Clear();
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