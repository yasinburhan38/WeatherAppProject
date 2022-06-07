using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using WeatherApp.Services.Login;
using Xamarin.Forms;

namespace WeatherApp.ViewModels.Dialogs
{
    public class LoginDialogViewModel
        : BaseViewModel, IDialogAware
    {
        private readonly ILoginService _loginService;
        public LoginDialogViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            
            LoginCommand = new Command<string>(LoginHandler);
            _loginService = loginService;
            LoginSucceeded = true; // default true
        }

        private async void LoginHandler(string obj)
        {
          LoginSucceeded = await _loginService.Login(Username, Wachtwoord);

            if (LoginSucceeded)
            {
                RequestClose(null);

            }
        }

        public string Username { get; set; }
        public string Wachtwoord { get; set; }
        public bool LoginSucceeded { get; set; }
        public Command LoginCommand { get; set; }


        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
