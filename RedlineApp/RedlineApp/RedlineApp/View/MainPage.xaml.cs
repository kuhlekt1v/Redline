/*
    File name: MainPage.xaml.cs
    Purpose:   Facilitate user interaction with page.
    Author:    Cody Sheridan
    Version:   1.0.1
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace RedlineApp.View
{
    public partial class MainPage : TabbedPage
    {
        // Initialize empty stack.
        protected Stack<Page> TabbedStack { get; private set; } = new Stack<Page>();

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnCurrentPageChanged()
        {
            // Get current page.
            var page = CurrentPage;
            if (page != null)
            {
                // Push page onto TabbedStack.
                TabbedStack.Push(page);
            }

            // Display alert if logout page selected.
            if (page.GetType() == typeof(LogoutPage))
            {
                bool response = await DisplayAlert("Logout", "Are you sure?", "Yes", "No");
                if (response)
                {
                    await Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    ReturnToLastPage();
                }
            }

            base.OnCurrentPageChanged();
        }


        protected bool ReturnToLastPage()
        {
            if (TabbedStack.Any())
            {
                // Remove last page from stack.
                TabbedStack.Pop();
            }

            if (TabbedStack.Any())
            {
                // Set current page equal last page in stack
                CurrentPage = TabbedStack.Pop();
                return true;
            }
            return true;
        }
    }
}