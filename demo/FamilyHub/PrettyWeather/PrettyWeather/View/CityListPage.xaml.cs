using PrettyWeather.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrettyWeather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityListPage : ContentPage
    {
        public CityListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(() =>
            {
                collection.Focus();
            });
        }

        private async void CityListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newCity = e.CurrentSelection[0] as City;
            (this.BindingContext as ViewModel.WeatherViewModel).SelectedCityItem = newCity;
            await Shell.Current.GoToAsync("//prettyWeather");
        }
    }
}