using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Core
{
    public interface IIdentityPrinipal
    {
        string Username { get; set; }
        bool IsLogged { get; set; }
    }
    public class IdentityPrincipal
        : IIdentityPrinipal
    {
        public string Username { get; set; }
        public bool IsLogged { get; set; }
    }
}
