﻿using Prism.Navigation;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class SettingsPageViewModel: BaseViewModel
    {
        

        public bool IsDarkMode { get; set; }
        public string Units { get; set; }

        

        

        public Command BackCommand { get; set; }
        public Command DarkModeToggleCommand { get; set; }
        public Command MetricCommand { get; set; }
        public Command ImperialCommand { get; set; }

        

       

        public SettingsPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            BackCommand = new Command(BackCommandHandler);
            DarkModeToggleCommand = new Command(DarkModeToggleCommandHandler);
            MetricCommand = new Command(MetricCommandHandler);
            ImperialCommand = new Command(ImperialCommandHandler);
        }

       

        

        private async void BackCommandHandler()
        {
            await _navigationService.GoBackAsync();
        }

        private void DarkModeToggleCommandHandler()
        {
            if (IsDarkMode)
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
                Preferences.Set("theme", "dark");
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
                Preferences.Set("theme", "light");
            }
        }

        private void MetricCommandHandler()
        {
            Units = "metric";
            Preferences.Set("units", Units);
        }

        private void ImperialCommandHandler()
        {
            Units = "imperial";
            Preferences.Set("units", Units);
        }

       

        

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            MainState = LayoutState.Loading;

            IsDarkMode = Application.Current.UserAppTheme.Equals(OSAppTheme.Dark);
            Units = Preferences.Get("units", "metric");

            MainState = LayoutState.None;
        }

        
    }
}
