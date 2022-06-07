using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace WeatherApp.Models
{
    public class CurrentCityModel:ObservableObject
    {
        private string _omschrijving;

        public string Omschrijving
        {
            get { return _omschrijving; }
            set {
                _omschrijving = value;
                OnPropertyChanged(nameof(Omschrijving));
            }
        }

    }
}
