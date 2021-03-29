/*
    File name: ManageAcount.xaml.cs
    Purpose:   Facilitate interaction with page and create dyanmic 
               view contained on single page.
    Author:    Cody Sheridan
    Version:   1.0.2
*/

using RedlineApp.ViewModel;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class ManageAccountPage : ContentPage
    {
        private ManageAccountViewModel accountViewModel;

        public ManageAccountPage()
        {
            InitializeComponent();
            accountViewModel = new ManageAccountViewModel();
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

            // Form grid components.
            Grid frameGrid = new Grid
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
            Grid fieldOneGrid = new Grid
            {
                RowSpacing = 5,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                }
            };
            Grid fieldTwoGrid = new Grid
            {
                RowSpacing = 5,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
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

            Label fieldOneLabel = new Label()
            {
                Text = "Label 1",
                Style = (Style)Application.Current.Resources["CategoryStyle"]
            };
            Label fieldTwoLabel = new Label()
            {
                Text = "Label 2",
                Style = (Style)Application.Current.Resources["CategoryStyle"]
            };

            Entry fieldOneEntry = new Entry()
            {
                Placeholder = "Field One",
                Style = (Style)Application.Current.Resources["EntryStyle"]
            };
            Entry fieldTwoEntry = new Entry()
            {
                Placeholder = "Field Two",
                Style = (Style)Application.Current.Resources["EntryStyle"]
            };

            fieldOneGrid.Children.Add(fieldOneLabel, 0, 0);
            fieldOneGrid.Children.Add(fieldOneEntry, 0, 1);
            fieldTwoGrid.Children.Add(fieldTwoLabel, 0, 0);
            fieldTwoGrid.Children.Add(fieldTwoEntry, 0, 1);

            submitGrid.Children.Add(backBtn, 0, 0);
            submitGrid.Children.Add(updateBtn, 1, 0);

            frameGrid.Children.Add(profileBtn, 0, 0);
            frameGrid.Children.Add(securityBtn, 0, 1);
            frameGrid.Children.Add(accountBtn, 0, 2);

            // Default categories.
            StackLayout mainContentInner = new StackLayout
            {
                Children =
                {
                    headingFrame,
                    frameGrid,
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
                DisplayUpdateForm("profileBtn", "First Name", "Last Name");       
            };

            securityBtn.Clicked += (sender, EventArgs) =>
            {
                HideDefaultView(sender, EventArgs, securityBtn.Text);
                DisplayUpdateForm("passwordBtn", "Password", "Confirm Password");

            };

            accountBtn.Clicked += (sender, EventArgs) =>
            {
                HideDefaultView(sender, EventArgs, accountBtn.Text);
                DisplayUpdateForm("accountBtn", "Username", "Email");
            };

            backBtn.Clicked += (sender, EventArgs) =>
            {
                HideUpdateForm();
                DisplayDefaultView();

            };

            
            void DisplayDefaultView()
            {
                headingLabel.Text = "General Settings";
                frameGrid.Children.Add(profileBtn);
                frameGrid.Children.Add(securityBtn);
                frameGrid.Children.Add(accountBtn);
            }

            void HideDefaultView(object sender, EventArgs e, string btnText)
            {
                headingLabel.Text = btnText;
                frameGrid.Children.Remove(profileBtn);  
                frameGrid.Children.Remove(securityBtn);  
                frameGrid.Children.Remove(accountBtn);
            }

            // Dynamically display requested form and print record from db.
            void DisplayUpdateForm(string itemName, string fieldOne, string fieldTwo)
            {
                List<string> list = new List<string>();
                string senderName = itemName;
                switch (senderName)
                {
                    case "profileBtn":
                        list = ManageAccountViewModel.GetCurrentUserName();
                        fieldTwoEntry.Keyboard = Keyboard.Default;
                        fieldOneEntry.IsPassword = false;
                        fieldTwoEntry.IsPassword = false;
                        break;
                    case "passwordBtn":
                        list = ManageAccountViewModel.GetCurrentUserPassword();
                        fieldTwoEntry.Keyboard = Keyboard.Default;
                        fieldOneEntry.IsPassword = true;
                        fieldTwoEntry.IsPassword = true;
                        break;
                    case "accountBtn":
                        list = ManageAccountViewModel.GetCurrentUserAccount();
                        fieldTwoEntry.Keyboard = Keyboard.Email;
                        fieldOneEntry.IsPassword = false;
                        fieldTwoEntry.IsPassword = false;
                        break;
                }

                // Display field one values.
                fieldOneLabel.Text = fieldOne;
                fieldOneEntry.Placeholder = fieldOne;
                fieldOneEntry.Text = list[0];

                // Display field two values.
                fieldTwoLabel.Text = fieldTwo;
                fieldTwoEntry.Placeholder = fieldTwo;     
                fieldTwoEntry.Text = list[1];


                frameGrid.Children.Add(fieldOneGrid, 0, 0);
                frameGrid.Children.Add(fieldTwoGrid, 0, 1);
                frameGrid.Children.Add(submitGrid, 0, 2);
            }

            void HideUpdateForm()
            {
                frameGrid.Children.Remove(fieldOneGrid);
                frameGrid.Children.Remove(fieldTwoGrid);
                frameGrid.Children.Remove(submitGrid);
            }

            base.OnAppearing();
        }

    }
}