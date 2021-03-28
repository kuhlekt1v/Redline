using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedlineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertPage : ContentPage
    {
        public AlertPage()
        {
            InitializeComponent();
        }

        public async void SendAlertButton_Clicked(object sender, EventArgs e)
        {
            await Sms.ComposeAsync(new SmsMessage("Please help", "911"));
        }
    }
}