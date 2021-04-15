/*
    File name: MapPage.xaml.cs
    Purpose:   Facilitate interaction with page.
    Author:    Cody Sheridan
    Version:   1.0.1
*/

using RedlineApp.Helpers;
using RedlineApp.Model;
using RedlineApp.Persistence;
using SQLite;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
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
            DisplayNearbyHospitals();

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
                        Position = userLocation,

                    };
                    userPin.Icon = BitmapDescriptorFactory.DefaultMarker(Color.FromHex("#0077ff"));
                    MapDisplay.Pins.Add(userPin);
                    MapDisplay.SelectedPin = userPin;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Please ensure location services are enabled.", "Close");
            }
        }


        // THIS WORKS! - ONLY COMMENTED OUT FOR IMPLEMENTATION OF VISUAL MAP.

        async void DisplayNearbyHospitals()
        {
            // REMOVE IN PRODUCTION - SECURITY RISK!
            var apiKey = "AIzaSyC7W4MIjUl-83OZv1Coz7gfzxNEiipTipM";

            // Show spinning activity indicator.
            //ActivityIndicatorStatus.IsVisible = true;

            // Access device location.
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);

            // Query to display hospitals within 30 mile radius of device location.
            var nearbyHospitals = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location.Latitude},{location.Longitude}&radius=49000&type=hospital&key={apiKey}";
            var result = await NearbyPlaceSearch(nearbyHospitals);


            //var listHospitals = new ObservableCollection<EmergencyService.Result>();

            //Add each item returned from GoogleAPI query to observable collection.

            foreach (var item in result.Results)
            {
                Position hospitalLocation = new Position(item.Geometry.Location.Lat, item.Geometry.Location.Lng);

                Pin hospitalPin = new Pin()
                {
                    Label = item.Name,
                    Address = item.Vicinity,
                    Position = hospitalLocation
                };

                MapDisplay.Pins.Add(hospitalPin);
            };

            





            //listHospitals.Add(item.);

            //// Attach results to list view.
            //LocationListView.ItemsSource = listHospitals;

            //// hide spinning activity indicator.
            //ActivityIndicatorStatus.IsVisible = false;
        }

        static async Task<EmergencyService.Root> NearbyPlaceSearch(string googleQuery)
        {
           // var pageToken = nextPageToken != null ? "&pagetoken=" + nextPageToken : null;
            var requestUri = string.Format(googleQuery);

            try
            {
                var restClient = new RestClient<EmergencyService.Root>();
                var result = await restClient.GetAsync(requestUri);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Nearby place search error: " + e.Message);
            }

            return null;
        }

        // Clear all pins and refresh for new user location.
        private void RefreshLocationBtnClicked(object sender, EventArgs e)
        {
            MapDisplay.Pins.Clear();
            DisplayUserLocation();
        }
    }
}