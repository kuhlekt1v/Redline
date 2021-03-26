using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedlineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        // Adding a button for Map with Coordinate include the location and option.
        private async void ButtonOpenWithCoordinate_Clicked(object sender, EventArgs e)
        {
            var location = new Location(41.40090, -73.28535);
            var options = new MapLaunchOptions { Name = "Newtown Municipal Center" };
            await Map.OpenAsync(location, options);

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
    }
}