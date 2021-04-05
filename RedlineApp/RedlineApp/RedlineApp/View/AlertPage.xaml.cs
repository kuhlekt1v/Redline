using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedlineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertPage : ContentPage
    {
        private SQLiteConnection _connection;

        public AlertPage()
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _connection.CreateTable<UserAccount>();
        }

        public async void SendAlertButton_Clicked(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLocationAsync();
            var activeUser = _connection.Table<UserAccount>().Where(x => x.ActiveUser).FirstOrDefault();
            await Sms.ComposeAsync(new SmsMessage($"{activeUser.FirstName} is here - {location}", "911"));
        }
    }
}