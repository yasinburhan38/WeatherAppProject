using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherAppProject.Views
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {

                        DesiredAccuracy = GeolocationAccuracy.Low,
                        Timeout = TimeSpan.FromSeconds(30)

                    });
                    
                    
                    
                    
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
                
                    
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Er is een fout: {ex.Message}");
            }
        }

        
    }
}
