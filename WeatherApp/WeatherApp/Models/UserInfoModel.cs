using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace WeatherApp.Models
{
    public class UserInfoModel : ObservableObject
    {
        private bool _hasNoEditRights;

        public bool HasNoEditRights
        {
            get { return _hasNoEditRights; }
            set
            {
                _hasNoEditRights = value;
                OnPropertyChanged(nameof(_hasNoEditRights));
            }
        }
    }
}
