using Prism;
using Prism.Ioc;
using Prism.Unity;
using WeatherAppProject.Core;
using WeatherAppProject.Services.Location;
using WeatherAppProject.ViewModels;
using WeatherAppProject.ViewModels.Dialogs;
using WeatherAppProject.Views;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
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

namespace WeatherAppProject
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        public new static App Current => Application.Current as App;

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var locations = await SecureStorage.GetAsync("locations");
            if (locations != null)
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(WeatherPage)}");
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
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            

            containerRegistry.RegisterForNavigation<NavigationPage>(nameof(NavigationPage));
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>(nameof(WelcomePage));
            containerRegistry.RegisterForNavigation<WeatherPage, WeatherPageViewModel>(nameof(WeatherPage));

            containerRegistry.RegisterForNavigation<AddLocationDialogViewModel, AddLocationDialogViewModel>();
        }
    }
}
