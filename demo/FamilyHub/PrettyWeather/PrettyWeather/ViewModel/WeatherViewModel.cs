using PrettyWeather.Model;
using PrettyWeather.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrettyWeather.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public WeatherViewModel()
        {
            _selectedCity = new City();
            _selectedCity.Name = string.Empty;

            _selectedCityItem = new City();
            _selectedCityItem.Name = string.Empty;

        }

        public WeatherViewModel(int temp)
        {
            _selectedCity = new City();
            _selectedCity.Name = string.Empty;

            _selectedCityItem = new City();
            _selectedCityItem.Name = string.Empty;

            Temp = temp;
        }

        private int _temp = 50;
        private string _weatherURL = "";
        private string _weatherURLFormat = "http://openweathermap.org/img/wn/{0}@2x.png";
        private bool _isBusy = false;

        public event PropertyChangedEventHandler PropertyChanged;

        Dictionary<int, string> _cityLandmarks = new Dictionary<int, string>()
        {
            { 1835847, "Seoul.png"},{ 2147714,"Sydney.png"},
            { 5392171, "SanJose.png"},{ 2643743,"London.png"},
            { 5391959, "SanFrancisco.png"},{ 2968815,"Paris.png"},
            { 5809844, "Seattle.png"},{ 6356055,"Barcelona.png"},
            { 5368361, "LosAngeles.png"},{ 2759794,"Amsterdam.png"},
            { 4407066, "StanleyCup.png"},{ 1273294,"Delhi.png"},
            { 5128581, "NewYork.png"},{ 1796236,"Shanghai.png"},
            { 4930956, "Boston.png"},{ 3451190,"RiodeJaneiro.png"},
            { 4140963, "Washington,D.C..png"},
            { 6173331, "Vancouver.png"},
        };
        
        string _landmarkSource;
        public string LandmarkSource
        {
            get
            {
                return _landmarkSource;
            }
            set
            {
                _landmarkSource = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LandmarkSource"));
            }
        }

        public string Date
        {
            get
            {
                return DateTime.Today.ToString("dddd, dd MMMM");
            }
        }

        ObservableCollection<City> _allcities;
        public ObservableCollection<City> AllCities
        {
            get { return _allcities; }
            set
            {
                _allcities = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllCities"));
            }
        }

        public int Temp
        {
            get { return _temp; }
            set
            {
                _temp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Temp"));
            }
        }

        // Weather Page uses this to show City data
        City _selectedCity;
        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                if (_selectedCity.Name != string.Empty)
                {
                    Temp = _selectedCity.CurrentWeather.Temp;
                    //WeatherURL = string.Format(_weatherURLFormat, _selectedCity.Weather[0].Icon);
                    _selectedCity.Weather[0].Icon = _selectedCity.Weather[0].Icon.Replace('n', 'd');
                    WeatherURL = string.Concat("icon/",_selectedCity.Weather[0].Icon,".png");
                    if (_cityLandmarks.ContainsKey(SelectedCity.Id))
                        LandmarkSource = string.Concat("landmarks/", _cityLandmarks[SelectedCity.Id]);
                    foreach (var c in AllCities)
                    {
                        c.CurrentCityName = value.Name;
                    }
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedCity"));
            }
        }

        public int SelectedItemIndex
        {
            get
            {
                return AllCities.IndexOf(SelectedCity);
            }
        }

        // Binding to SelectedItem in CollectionView
        City _selectedCityItem;
        public City SelectedCityItem
        {
            get => _selectedCityItem;
            set
            {
                if (value.Name.Equals("footer"))
                    return;
                _selectedCityItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedCityItem"));
                SelectedCity = _selectedCityItem;
            }
        }

        public string WeatherURL
        {
            get
            {
                return _weatherURL;
            }
            set
            {
                _weatherURL = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeatherURL"));
            }
        }

        bool _useCelsius;
        public bool UseCelsius
        {
            get => _useCelsius;
            set
            {
                _useCelsius = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UseCelsius"));
            }
        }

        public bool IsBusy
        {
            get {
                return _isBusy;
            }
            private set
            {
                _isBusy = value;
            }
        }

        public async Task GetGroupedWeatherAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                List<string> allCities = WeatherService.WORLD_CITIES;

                CitiesWeatherRoot payload = null;
                var units = Units.Imperial;//_useCelsius ? Units.Metric : Units.Imperial;
                payload = await WeatherService.Instance.GetWeatherAsync(allCities, units);
                _allcities = new ObservableCollection<City>(payload.CityList);

                if (_allcities.Count > 0)
                    _allcities[0].Name = "Seoul";
                PropertyChanged(this, new PropertyChangedEventArgs("AllCities"));

                if (_allcities.Count > 0)
                    SelectedCityItem = _allcities[0];

                _allcities.Add(new City()
                {
                    Name = "footer"
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //Temp = "Unable to get Weather";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
