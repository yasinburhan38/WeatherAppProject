using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WeatherAppProject.ViewModels
{
    public class WeatherPageViewModel : ViewModelBase,INotifyPropertyChanged
    {
        public WeatherPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
          

    }
    }
}
