/*
    File name: ManageAcount.xaml.cs
    Purpose:   Facilitate interaction with page and create dyanmic view.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using System;
using Xamarin.Forms;

namespace RedlineApp.View
{
    // REFERENCE: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/button
    public partial class ManageAccountPage : ContentPage
    {
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
            };

            // Main content area.
            Frame mainContentArea = new Frame
            {
                CornerRadius = 13,
                Padding = 0,
                Margin = new Thickness(0, 20, 0, -10),
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = (Color)Application.Current.Resources["BackgroundColor"],
            };

            // Content heading area.
            Frame headingFrame = new Frame
            {
                CornerRadius = 4,
                HasShadow = true,
                BackgroundColor = (Color)Application.Current.Resources["SecondaryColor"]
            };

            // General settings label.
            Label headingLabel = new Label
            {
                Text = "General Settings",
                Style = (Style)Application.Current.Resources["CategoryTitleStyle"]
            };

            // Compose heading.
            headingFrame.Content = headingLabel;

            Grid buttonGrid = new Grid
            {
                RowSpacing = 20,
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition { Width = new GridLength(100) },
                    new ColumnDefinition()
                },
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition()
                }
            };

            Button accountSettingsBtn = new Button
            {
                Text = "Account Settings",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Style = (Style)Application.Current.Resources["PrimaryButton"]
            };

            buttonGrid.Children.Add(accountSettingsBtn);


            
            // Default categories.
            StackLayout defaultCategories = new StackLayout
            {
                Children =
                {
                    headingFrame,
                    buttonGrid,
                    //new Label {Text = "Account Security", Padding = new Thickness(20, 5, 0, 5), Style = (Style)Application.Current.Resources["CategoryStyle"]},
                    //new Label {Text = "Delete Account", Padding = new Thickness(20, 5, 0, 5), Style = (Style)Application.Current.Resources["CategoryStyle"]},
                }
            };

            // Compose main content view.
            mainContentArea.Content = defaultCategories;

            // Compose complete view.
            outerStack.Children.Add(mainContentArea);
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


//    // General account settings buttons.
//    Label updateAccountLbl = new Label
//    {
//        Text = "Update Account",
//        Padding = new Thickness(20, 5, 0, 5),
//        Style = (Style)Application.Current.Resources["CategoryStyle"]
//    };

//    var updateAccountTap = new TapGestureRecognizer();
//    updateAccountTap.Tapped += (object sender, EventArgs e) =>
//            {
//                outerStack.Children.Clear();
//            };
//updateAccountLbl.GestureRecognizers.Add(updateAccountTap);
}