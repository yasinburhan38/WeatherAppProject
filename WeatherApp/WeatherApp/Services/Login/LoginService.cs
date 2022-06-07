using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using WeatherApp.Core;

namespace WeatherApp.Services.Login
{
    public class LoginService
        : ILoginService
    {
        private readonly HttpClientFactory _httpClientFactory;
        private readonly IIdentityPrinipal _identityPrinipal;

        public LoginService(HttpClientFactory httpClientFactory, IIdentityPrinipal identityPrinipal)
        {
            _httpClientFactory = httpClientFactory;
            _identityPrinipal = identityPrinipal;
        }
        private const string BASE_URL = "http://10.23.81.81:45455/";
        private const string LOGIN = "Authorization?username={0}&password={1}";

        public async Task<bool> Login(string username, string password)
        {
            var path = string.Format(LOGIN, username, password);
            var response = await _httpClientFactory.GetHttpClient().GetAsync($"{BASE_URL}{path}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                _identityPrinipal.Username = json["username"].ToString();
                _identityPrinipal.IsLogged = true;
                return true;
            }

            return false;
        }
    }
}
