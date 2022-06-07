using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using WeatherApp.Core;
using WeatherApp.Services.LocalSettings;
using WeatherApp.Services.Location;
using WeatherApp.Services.Login;
using WeatherApp.Services.Weather;
using WeatherApp.ViewModels;
using WeatherApp.ViewModels.Dialogs;
using WeatherApp.Views;
using WeatherApp.Views.Dialogs;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: ExportFont("FontAwesome.ttf", Alias = "FontAwesome")]

[assembly: ExportFont("FrankRuhlLibre-Light.ttf", Alias = "FrankRuhle_Light")]
[assembly: ExportFont("FrankRuhlLibre-Regular.ttf", Alias = "FrankRuhle_Regular")]
[assembly: ExportFont("FrankRuhlLibre-Medium.ttf", Alias = "FrankRuhle_Medium")]
[assembly: ExportFont("FrankRuhlLibre-Bold.ttf", Alias = "FrankRuhle_Bold")]
[assembly: ExportFont("FrankRuhlLibre-Black.ttf", Alias = "FrankRuhle_Black")]

[assembly: ExportFont("Barlow-Light.ttf", Alias = "Barlow_Light")]
[assembly: ExportFont("Barlow-Regular.ttf", Alias = "Barlow_Regular")]
[assembly: ExportFont("Barlow-Medium.ttf", Alias = "Barlow_Medium")]
[assembly: ExportFont("Barlow-SemiBold.ttf", Alias = "Barlow_SemiBold")]

namespace WeatherApp
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        public new static App Current => Application.Current as App;

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SetAppTheme();

            var locations = await SecureStorage.GetAsync("locations");
            if (locations != null)
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WelcomePage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WelcomePage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(new HttpClientFactory());
            containerRegistry.Register<ILocationService, LocationService>();
            containerRegistry.Register<ILocalSettingsService, LocalSettingsService>();
            containerRegistry.Register<IWeatherService, WeatherService>();
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.RegisterSingleton<IIdentityPrinipal, IdentityPrincipal>();

            containerRegistry.RegisterForNavigation<NavigationPage>(nameof(NavigationPage));
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>(nameof(WelcomePage));
            containerRegistry.RegisterForNavigation<WeatherPage, WeatherPageViewModel>(nameof(WeatherPage));
            containerRegistry.RegisterForNavigation<YourLocationsPage, YourLocationsPageViewModel>(nameof(YourLocationsPage));
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>(nameof(SettingsPage));

            containerRegistry.RegisterDialog<AddLocationDialog, AddLocationDialogViewModel>();
            containerRegistry.RegisterDialog<LoginDialog, LoginDialogViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void SetAppTheme()
        {
            var theme = Preferences.Get("theme", string.Empty);
            if(string.IsNullOrEmpty(theme) || theme == "light")
            {
                Application.Current.UserAppTheme = OSAppTheme.Light;
            }
            else
            {
                Application.Current.UserAppTheme = OSAppTheme.Dark;
            }
        }
    }
}
