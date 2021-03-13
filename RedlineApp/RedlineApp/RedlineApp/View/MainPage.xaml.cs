/*
    File name: MainPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Cody Sheridan
    Version:   1.0.0
*/

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedlineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}