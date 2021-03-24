using System;

using Xamarin.Forms;

namespace RedlineApp.View
{
    // REFERENCE: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/button
    public partial class ManageAccountPage : ContentPage
    {
        StackLayout outerStack;
        Frame mainContentArea;

        public ManageAccountPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // View area other than status bar.
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
                BackgroundColor = (Color)Application.Current.Resources["BackgroundColor"],
            };


            // General account settings buttons.
            Label updateAccountLbl = new Label
            {
                Text = "Update Account",
                Padding = new Thickness(20, 5, 0, 5),
                Style = (Style)Application.Current.Resources["CategoryStyle"]
            };

            var updateAccountTap = new TapGestureRecognizer();
            updateAccountTap.Tapped += (object sender, EventArgs e) =>
            {
                 DisplayAlert("Success", "Link tapped", "ok");
            };
            updateAccountLbl.GestureRecognizers.Add(updateAccountTap);




            // Default categories
            StackLayout defaultCategories = new StackLayout
            {

                Children =
                {
                    // General account settings.
                    new Label {Text = "General Settings", Padding = new Thickness(10, 20, 0, 10), Style = (Style)Application.Current.Resources["CategoryTitleStyle"]},
                    updateAccountLbl,
                    new Label {Text = "Account Security", Padding = new Thickness(20, 5, 0, 5), Style = (Style)Application.Current.Resources["CategoryStyle"]},
                    new Label {Text = "Delete Account", Padding = new Thickness(20, 5, 0, 5), Style = (Style)Application.Current.Resources["CategoryStyle"]},
                }
            };

            // Compose main content view.
            mainContentArea.Content = defaultCategories;

            Button hideDefaultBtn = new Button
            {
                Text = "Hide TEST"
                // Define properties here
            };

            // Need to add ManageAccountViewModel
            hideDefaultBtn.Clicked += HideDefaultCategories;

            StackLayout testButton = new StackLayout
            {
                Children =
                {
                    hideDefaultBtn
                }
            };



            // Compose the view.
            outerStack.Children.Add(mainContentArea);
            outerStack.Children.Add(testButton);

            Content = outerStack;
        }

        void HideDefaultCategories(object sender, EventArgs e)
        {
            // Need to clear children of mainContentArea
            // which is a child of outerStack.
            
        }

        async void Test(object sender, EventArgs e)
        {
            await DisplayAlert("X", "X", "X");
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