using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Services.Login
{
    public interface ILoginService
    {
        Task<bool> Login(string username, string password);
    }
}
