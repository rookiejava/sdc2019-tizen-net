using PrettyWeather.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrettyWeather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrettyWeatherPage : ContentPage
    {
        public PrettyWeatherPage()
        {
            InitializeComponent();
                Console.WriteLine($"######### device width: {Application.Current.MainPage.Width}");
                Console.WriteLine($"######### thiswidth: {this.Width}");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!string.IsNullOrEmpty((collection.SelectedItem as City).Name))
                {
                    collection.ScrollTo((this.BindingContext as ViewModel.WeatherViewModel).SelectedItemIndex, -1, ScrollToPosition.Center, false);
                }
                else
                {
                    collection.Focus();
                }
            });
        }

        private async void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty((collection.SelectedItem as City).Name))
            {
                Device.BeginInvokeOnMainThread(()=>
                {
                    collection.ScrollTo((this.BindingContext as ViewModel.WeatherViewModel).SelectedItemIndex, -1, ScrollToPosition.Center, true);
                });
            }

            var city = e.CurrentSelection[0] as City;
            if (city.Name.Equals("footer"))
            {
                await Shell.Current.GoToAsync("//cityListPage");
            }
        }

        private void Label_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == Label.FormattedTextProperty.PropertyName)
            {
                var label = sender as Label;
                if(label.FormattedText.ToString().StartsWith((this.BindingContext as ViewModel.WeatherViewModel).SelectedCity.Name))
                {
                    Device.BeginInvokeOnMainThread(()=>
                    {
                        (label.Parent as Xamarin.Forms.Grid).Children[0].Focus();
                    });
                }
            }
        }

        private void ViewHolder_Focused(object sender, FocusEventArgs e)
        {
            if(e.IsFocused)
            {
                ((sender as Xamarin.Forms.View).Parent.Parent as Grid).Children[0].BackgroundColor = Color.FromRgba(244, 244, 244, 200);
            }
            else
            {
                ((sender as Xamarin.Forms.View).Parent.Parent as Grid).Children[0].BackgroundColor = Color.Transparent;
            }
        }
    }
}