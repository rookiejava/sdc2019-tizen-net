using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using PrettyWeather.Model;

namespace PrettyWeather.View
{
    class CityDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Cities { get; set; }
        public DataTemplate FooterItem { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((City)item).Name.Contains("footer") ? FooterItem : Cities;
        }
    }
}
