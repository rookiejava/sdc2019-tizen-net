using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using PrettyWeather.Model;

namespace PrettyWeather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        public MainPage()
        {
            InitializeComponent();
            var viewModel = BindingContext as ViewModel.WeatherViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _ = (BindingContext as ViewModel.WeatherViewModel).GetGroupedWeatherAsync();
        }

        private void OnImageClicked(object sender, EventArgs args)
        {
            var viewModel = BindingContext as ViewModel.WeatherViewModel;
            viewModel.SettingEnabled = true;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            var viewModel = BindingContext as ViewModel.WeatherViewModel;
            viewModel.SelectedCity = (City)args.Item;
            viewModel.SettingEnabled = false;
        }
    }
}