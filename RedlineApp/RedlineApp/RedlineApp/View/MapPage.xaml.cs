using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using System;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using Map = Xamarin.Essentials.Map;

namespace RedlineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private SQLiteConnection _connection;
        private TableQuery<UserAccount> _data;

        public MapPage()
        {
            _connection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _connection.CreateTable<UserAccount>();
            _data = _connection.Table<UserAccount>();

            InitializeComponent();
            DisplayUserLocation();

        }


        public async void DisplayUserLocation()
        {
            var user = _data.Where(x => x.ActiveUser).FirstOrDefault();

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLocationAsync(request);
                if(location != null)
                {
                    // Display 30 mile radius around user location
                    Position userLocation = new Position(location.Latitude, location.Longitude);
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(userLocation, Distance.FromMiles(30));
                    MapDisplay.MoveToRegion(mapSpan);

                    // Display user current exact location
                    Pin userPin = new Pin
                    {
                        Label = $"{user.FirstName} {user.LastName}",
                        Address = "You are here.",
                        Type = PinType.Place,
                        Position = userLocation

                    };
                    MapDisplay.Pins.Add(userPin);
                    MapDisplay.SelectedPin = userPin;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Please ensure location services are enabled.", "Close");
            }
        }



        // Adding another button for Map with placemark include the location and option.
        private async void ButtonOpenWithPlacemark_Clicked(object sender, EventArgs e)
        {
            var placemark = new Placemark
            {
                CountryName = "United State",
                AdminArea = "CT",
                Thoroughfare = "Newtown Municipal Center",
                Locality = "Fairfield"
            };
            var options = new MapLaunchOptions { Name = "Newtown Municipal Center" };
            await Map.OpenAsync(placemark, options);
        }

        // Clear all pins and refresh for new user location.
        private void RefreshLocationBtnClicked(object sender, EventArgs e)
        {
            MapDisplay.Pins.Clear();
            DisplayUserLocation();
        }
    }
}