using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using WeatherApp.Core;
using Xamarin.Forms;

namespace WeatherApp.Converters
{
    public class LoggedinConverter : IValueConverter
    {
        private readonly IIdentityPrinipal _identityPrinipal;

        public LoggedinConverter(IIdentityPrinipal identityPrinipal)
  
        {
            _identityPrinipal = identityPrinipal;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && _identityPrinipal != null && _identityPrinipal.IsLogged)
            {
                return "Log out";
            }
            return "Log in";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
