using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeatherAppProject.Services.Location;
using WeatherAppProject.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherAppProject.ViewModels
{
    public class WelcomePageViewModel : ViewModelBase
    {   private ILocationService _locationService;
        public string CurrentLocation { get; set; }
        public Command UseCurrentLocationCommand { get; set; }
        public Command AddLocationCommand { get; set; }
        public WelcomePageViewModel(INavigationService navigationService,

            ILocationService locationService)
            : base(navigationService)
        {
            _locationService = locationService;



            UseCurrentLocationCommand = new Command(UseCurrentLocationCommandHandler);
            AddLocationCommand = new Command(AddLocationCommandHandler);
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            
        }
        private async void UseCurrentLocationCommandHandler()
        {
            if (HasNoInternetConnection)
                return;
            MainState = LayoutState.Loading;
            try
            {
                var location = await _locationService.GetCurrentLocationCoordinates();
                if (location != null)
                {
                    var placemark = await _locationService.GetCurrentLocationName(location.Latitude, location.Longitude);
                    await SaveLocationAndPlacemark(location, placemark);
                    await _navigationService.NavigateAsync(nameof(WeatherPage));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                MainState = LayoutState.None;
            }
        }
        private async void AddLocationCommandHandler()
        {
            var param = new DialogParameters()
            {
                { "fromPage" , "welcome" }
            };
            
        }

        private Task SaveLocationAndPlacemark(Location location, Placemark placemark)
        {
            throw new NotImplementedException();
        }
       
    }
}
