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
            Frame mainContentFrame = new Frame
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

            Grid innerGrid = new Grid
            {
                RowSpacing = 20,
                Margin = new Thickness(20, 0, 20, 0),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                }
            };
            Grid submitGrid = new Grid
            {
                ColumnSpacing = 20,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition{ Width = new GridLength(70, GridUnitType.Star) }
                }
            };

            // Individual form components.
            Button profileBtn = new Button()
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
            Button backBtn = new Button
            {
                Text = "<",
                Style = (Style)Application.Current.Resources["PrimaryButton"]
            };
            Button updateBtn = new Button
            {
                Text = "Update",
                Style = (Style)Application.Current.Resources["PrimaryButton"]
            };

            Entry firstNameEntry = new Entry()
            {
                Placeholder = "First Name",
                Style = (Style)Application.Current.Resources["EntryStyle"]
            };
            Entry lastNameEntry = new Entry()
            {
                Placeholder = "Last Name",
                Style = (Style)Application.Current.Resources["EntryStyle"]
            };

            Label firstNameLbl = new Label()
            {
                Text = "First Name",
                Margin = new Thickness(0, 0, 0, -50),
                Style = (Style)Application.Current.Resources["CategoryStyle"]
            };

            Label lastNameLbl = new Label()
            {
                Text = "Last Name",
                Margin = new Thickness(0, 0, 0, -50),
                Style = (Style)Application.Current.Resources["CategoryStyle"]
            };

            submitGrid.Children.Add(backBtn, 0, 0);
            submitGrid.Children.Add(updateBtn, 1, 0);

            innerGrid.Children.Add(profileBtn, 0, 0);
            innerGrid.Children.Add(securityBtn, 0, 1);
            innerGrid.Children.Add(accountBtn, 0, 2);

            // Default categories.
            StackLayout mainContentInner = new StackLayout
            {
                Children =
                {
                    headingFrame,
                    innerGrid,
                }
            };

            // Compose main content view.
            mainContentFrame.Content = mainContentInner;

            // Compose complete view.
            outerStack.Children.Add(mainContentFrame);
            Content = outerStack;

            // Button click events.
            profileBtn.Clicked += (sender, EventArgs) =>
            {
                HideDefaultView(sender, EventArgs, profileBtn.Text);
                DisplayProfileForm();
            };

            securityBtn.Clicked += (sender, EventArgs) =>
            {
                HideDefaultView(sender, EventArgs, securityBtn.Text);
            };

            accountBtn.Clicked += (sender, EventArgs) =>
            {
                HideDefaultView(sender, EventArgs, accountBtn.Text);
            };

            // Swap default category view for appropriate form.
            void HideDefaultView(object sender, EventArgs e, string btnText)
            {
                headingLabel.Text = btnText;
                innerGrid.Children.Remove(profileBtn);  
                innerGrid.Children.Remove(securityBtn);  
                innerGrid.Children.Remove(accountBtn);
            }

            void DisplayProfileForm()
            {
                innerGrid.Children.Add(firstNameLbl, 0, 0);
                innerGrid.Children.Add(firstNameEntry, 0, 1);
                innerGrid.Children.Add(lastNameLbl, 0, 2);
                innerGrid.Children.Add(lastNameEntry, 0, 3);
                innerGrid.Children.Add(submitGrid, 0, 4);
            }


            //void DisplayForm()
            //{
            //    mainContentInner.Conte

            //    // Compose main content view.
            //    mainContentFrame.Content = mainContentInner;
            //    outerStack.Children.Add(mainContentFrame);
            //    Content = outerStack;
            //}

            base.OnAppearing();
        }

    }
}