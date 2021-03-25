/*
    File name: ManageAcount.xaml.cs
    Purpose:   Facilitate interaction with page and create dyanmic view.
    Author:    Cody Sheridan
    Version:   1.0.1
*/

using System;
using System.Threading.Tasks;
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

        // Ensure no active users on page load.
        protected override void OnAppearing()
        {
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
                Margin = new Thickness(20, 0, 20, 0),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition()
                }
            };

            Button profileBtn = new Button
            {
                Text = "Profile",
                Style = (Style)Application.Current.Resources["PrimaryButton"]
            };

            Button securityBtn = new Button
            {
                Text = "Security",
                Style = (Style)Application.Current.Resources["PrimaryButton"]
            };

            Button accountBtn = new Button
            {
                Text = "Account",
                Style = (Style)Application.Current.Resources["PrimaryButton"]
            };

            buttonGrid.Children.Add(profileBtn, 0, 0);
            buttonGrid.Children.Add(securityBtn, 0, 1);
            buttonGrid.Children.Add(accountBtn, 0, 2);

            // Default categories.
            StackLayout defaultCategories = new StackLayout
            {
                Children =
                {
                    headingFrame,
                    buttonGrid,
                }
            };

            // Compose main content view.
            mainContentArea.Content = defaultCategories;

            // Compose complete view.
            outerStack.Children.Add(mainContentArea);
            Content = outerStack;

            // Button click events.
            profileBtn.Clicked += (sender, EventArgs) =>
            {
                DisplayInputForm(sender, EventArgs, profileBtn.Text);
            };

            securityBtn.Clicked += (sender, EventArgs) =>
            {
                DisplayInputForm(sender, EventArgs, securityBtn.Text);
            };

            accountBtn.Clicked += (sender, EventArgs) =>
            {
                DisplayInputForm(sender, EventArgs, accountBtn.Text);
            };

            // Swap default category view for appropriate form.
            void DisplayInputForm(object sender, EventArgs e, string btnText)
            {
                headingLabel.Text = btnText;
                defaultCategories.Children.RemoveAt(1);
            }

            base.OnAppearing();
        }

    }
}