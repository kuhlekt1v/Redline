using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace RedlineApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            Pin hospital1 = new Pin()
            {
                Type = PinType.Place,
                Label = "Park City Hospital",
                Address = "Fairfeild, CT, United State",
                Position = new Position(41.1709301, -73.1959455),
            };

            var hospital2 = new Pin()
            {
                Type = PinType.Place,
                Label = "Atrium Helath Care Center",
                Address = "New Heaven, CT, United Statee",
                Position = new Position(41.3092638, -72.948714),
            };

            var hospital3 = new Pin()
            {
                Type = PinType.Place,
                Label = "Danbury Hospital",
                Address = "Fairfield, CT, United Statee",
                Position = new Position(41.7037059, -73.4459553)
            };

            var hospital4 = new Pin()
            {
                Type = PinType.Place,
                Label = "Doctor Barnes Sanitarium (historical)",
                Address = "Fairfeild, CT, United Statee",
                Position = new Position(41.0742635, -73.553735),
            };

            var hospital5 = new Pin()
            {
                Type = PinType.Place,
                Label = "Bridgport Hospital",
                Address = "Fairfeild, CT, United State",
                Position = new Position(41.1731526, -73.1642775),
            };

            var hospital6 = new Pin()
            {
                Type = PinType.Place,
                Label = "Fairfield State Hospital",
                Address = "Fairfeild, CT, United State",
                Position = new Position(41.400929, -73.2851166),
            };

            var hospital7 = new Pin()
            {
                Type = PinType.Place,
                Label = "Gaylord Hospital",
                Address = "Fairfeild, CT, United State",
                Position = new Position(41.4734306, -72.8556563),
            };

            var hospital8 = new Pin()
            {
                Type = PinType.Place,
                Label = "Griffin Hospital",
                Address = "New Heaven, CT , United State",
                Position = new Position(41.3350967, -73.0898308),
            };

            var hospital9 = new Pin()
            {
                Type = PinType.Place,
                Label = "Saint Lukes Hospital",
                Address = "Fairfeild, CT, United State",
                Position = new Position(41.0706524, -73.6951296),
            };

            var hospital10 = new Pin()
            {
                Type = PinType.Place,
                Label = "Norwalk Hospital",
                Address = "Fairfeild, CT,  United State",
                Position = new Position(41.1106523, -73.4217861),
            };

            map.Pins.Add(hospital1);
            map.Pins.Add(hospital2);
            map.Pins.Add(hospital3);
            map.Pins.Add(hospital4);
            map.Pins.Add(hospital5);
            map.Pins.Add(hospital6);
            map.Pins.Add(hospital7);
            map.Pins.Add(hospital8);
            map.Pins.Add(hospital9);
            map.Pins.Add(hospital10);

           // map.MoveToRegion(MapSpan.FromCenterAndRadius(hospital1.Position, Distance.FromMiles(3)));
        }

       
       
    }
}