using Newtonsoft.Json;
using Prism.Events;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Core;
using WeatherApp.Events;
using WeatherApp.Helpers;
using WeatherApp.Models;
using WeatherApp.Services.Weather;
using WeatherApp.Views;
using WeatherApp.Views.Dialogs;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeatherPageViewModel : BaseViewModel
    {
        

        private readonly IWeatherService _weatherService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IIdentityPrinipal _identityPrinipal;
        private IDialogService _dialogService;

        private double _currentLat;
        private double _currentLon;

        

        

        public CurrentCityModel CurrentCity { get; set; }


        public string CurrentCountry { get; set; }
        public double CurrentTemp { get; set; }
        public CurrentWeatherModel CurrentWeather { get; set; }
        public WeatherDetailsModel SelectedHour { get; set; }
        public bool IsRefreshing { get; set; }
        public ObservableCollection<MenuItemModel> MenuItems { get; set; }
        public LocationModel SelectLocation { get; set; }
        public List<LocationModel> Locations { get; set; }
        public UserInfoModel UserInfo { get; set; }

        

        

        public Command RefreshCommand { get; set; }
        public Command TryAgainCommand { get; set; }
        public Command SideMenuCommand { get; set; }
        public Command MenuCommand { get; set; }
        public Command AuthorizeCommand { get; set; }

        

        

        public WeatherPageViewModel(
            INavigationService navigationService,
            IWeatherService weatherService,
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            IIdentityPrinipal identityPrinipal) : base(navigationService)
        {
            _weatherService = weatherService;
            _eventAggregator = eventAggregator;
            RefreshCommand = new Command(RefreshCommandHandler);
            TryAgainCommand = new Command(TryAgainCommandHandler);
            SideMenuCommand = new Command<string>(SideMenuCommandHandler);
            MenuCommand = new Command(MenuCommandHandler);
            AuthorizeCommand = new Command<string>(AuthorizationHandler);

            MenuItems = MenuItemsHelper.Items;
            _dialogService = dialogService;
            UserInfo = new UserInfoModel() { HasNoEditRights = !identityPrinipal.IsLogged };
            _identityPrinipal = identityPrinipal;
        }


        

        

        private async void RefreshCommandHandler()
        {
            if (HasNoInternetConnection)
            {
                IsRefreshing = false;
                return;
            }
            IsRefreshing = true;
            await GetCurrentWeather();
            IsRefreshing = false;
        }

        private async void TryAgainCommandHandler()
        {
            if (HasNoInternetConnection)
                return;
            MainState = LayoutState.Loading;
            await GetCurrentWeather();
        }

        private async void SideMenuCommandHandler(string label)
        {
            _eventAggregator.GetEvent<MenuEvent>().Publish();
            switch(label)
            {
                case "Locations": await _navigationService.NavigateAsync(nameof(YourLocationsPage)); break;
                case "Settings": await _navigationService.NavigateAsync(nameof(SettingsPage)); break;
                case "Welcome": await _navigationService.NavigateAsync(nameof(WelcomePage)); break;
            }
        }

        private void MenuCommandHandler()
        {
            _eventAggregator.GetEvent<MenuEvent>().Publish();
        }
        private async void AuthorizationHandler(string value)
        {
            var param = new DialogParameters()
            {
                { "fromPage" , "welcome" }
            };
            if(value == "Log out")
            {
                _identityPrinipal.IsLogged = false;
                _identityPrinipal.Username = null;
                await App.Current.MainPage.DisplayAlert("Alert", "U bent uitgelogd", "OK");
                await _navigationService.NavigateAsync(nameof(WelcomePage));

            }
            else
            {
                await _dialogService.ShowDialogAsync(nameof(LoginDialog), param);
            }
        }


        private void OnSelectedHourChanged()
        {
            CurrentWeather.hourlyWeatherForecast.ToList().ForEach(a => a.isActive = false);
            AnimateTemp(SelectedHour.temp);
            SelectedHour.isActive = true;
        }

        

        

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await GetSelectedPlacemarkAndLocation();
            await GetCurrentWeather();
        }

       

        

        private void AnimateTemp(double temp)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var sign = temp > CurrentTemp ? 1 : -1;

            Device.StartTimer(TimeSpan.FromSeconds(1 / 1000f), () =>
            {
                double t = stopwatch.Elapsed.TotalMilliseconds % 500 / 1000;
                if(sign == 1)
                {
                    CurrentTemp = Math.Min((double)temp, (double)t + CurrentTemp);
                    if (CurrentTemp >= (double)temp)
                    {
                        stopwatch.Stop();
                        return false;
                    }
                }
                else
                {
                    CurrentTemp = Math.Max((double)temp, CurrentTemp - (double)t);
                    if (CurrentTemp <= (double)temp)
                    {
                        stopwatch.Stop();
                        return false;
                    }
                }


                return true;
            });
        }

        private async Task GetSelectedPlacemarkAndLocation()
        {
            try
            {
                var listLocJson = await SecureStorage.GetAsync("locations");
                Locations = JsonConvert.DeserializeObject<List<LocationModel>>(listLocJson);
                SelectLocation = Locations.FirstOrDefault<LocationModel>(l => l.Selected);

                if (SelectLocation != null)
                {
                    CurrentCity = new CurrentCityModel();
                    CurrentCity.Omschrijving = SelectLocation.Locality;
                    CurrentCountry = SelectLocation.CountryName;
                    _currentLat = SelectLocation.Latitude;
                    _currentLon = SelectLocation.Longitude;
                    CurrentCity.PropertyChanged += CurrentCity_PropertyChanged;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void CurrentCity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var city = (CurrentCityModel)sender;

            UpdateLocation(city.Omschrijving);
        }

        private void UpdateLocation(string omschrijving)
        {
            Locations.FirstOrDefault(x => x.Selected).Locality = omschrijving;
            SecureStorage.SetAsync("locations", JsonConvert.SerializeObject(Locations));
        }

        private async Task GetCurrentWeather()
        {
            try
            {
                var units = Preferences.Get("units", "metric");
                CurrentWeather = await _weatherService.GetCurrentWeatherAndHourlyForecastByLatLon(_currentLat, _currentLon, units);
                if(CurrentWeather != null)
                {
                    SelectedHour = CurrentWeather.hourlyWeatherForecast[0];
                    SelectedHour.isActive = true;
                    MainState = LayoutState.None;
                }
                else
                {
                    MainState = LayoutState.Error;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                MainState = LayoutState.Error;
            }
        }

       
    }
}
