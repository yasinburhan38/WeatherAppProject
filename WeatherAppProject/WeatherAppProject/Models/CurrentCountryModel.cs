using Xamarin.CommunityToolkit.ObjectModel;
namespace WeatherAppProject.Models
{
    public class CurrentCountryModel: ObservableObject
    {
        private string _omschrijving;

        public string Omschrijving
        {
            get { return _omschrijving; }
            set
            {
                _omschrijving = value;
                OnPropertyChanged(nameof(Omschrijving));
            }
        }

       
    }
}
