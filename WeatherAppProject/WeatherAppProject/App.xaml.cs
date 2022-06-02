using Prism;
using Prism.Ioc;
using WeatherAppProject.ViewModels;
using WeatherAppProject.Views;
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
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/WeatherPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
            containerRegistry.RegisterForNavigation<WeatherPage, WeatherPageViewModel>();
        }
    }
}
