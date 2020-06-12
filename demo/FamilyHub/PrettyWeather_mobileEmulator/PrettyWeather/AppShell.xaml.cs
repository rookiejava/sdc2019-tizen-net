using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrettyWeather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _ = (BindingContext as ViewModel.WeatherViewModel).GetGroupedWeatherAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            var result = DisplayAlert("Exit", "Do you want to exit?", "Yes", "No").ContinueWith(task =>
            {
                if(task.Result)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.Quit();
                    });
                }
            });
            return true;
        }
    }
}