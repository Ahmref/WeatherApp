using System.Collections.ObjectModel;
using System.ComponentModel;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }
        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }
        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }
        public ObservableCollection<City> Cities { get; set; }
        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }
        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
            Cities.Clear();
            foreach (var City in cities)
            {
                Cities.Add(City);
            }


        }
        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "Dresden"
                };
                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Party Cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "18"
                        }
                    }
                };
            }
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        public SearchCommand SearchCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
