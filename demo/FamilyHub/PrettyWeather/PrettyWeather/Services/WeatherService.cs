using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using PrettyWeather.Model;
using static Newtonsoft.Json.JsonConvert;

namespace PrettyWeather.Services
{
    public enum Units
    {
        Imperial,
        Metric
    }

    public class WeatherService
    {
        const string WeatherCitiesUri = "http://api.openweathermap.org/data/2.5/group?id={0}&units={1}&appid=cd6084286640ac2ef0b60b4eb006a477";

        public static List<string> WORLD_CITIES = new List<string>() {
            "1835847", // "Seoul"
            "5392171", // "San Jose"
            "5391959", // "San Francisco"
            "5809844", // "Seattle"
            "4407066", // "Saint Louis" -> Stanley Cup
            "5128581", // "New York"
            "4930956", // "Boston"
            "4140963", // "Washington, D.C."
            "6173331", // "Vancouver"
            "2147714", // "Sydney"
            "2643743", // "London"
            "2968815", // "Paris"
            "6356055", // "Barcelona"
            "2759794", // "Amsterdam"
            "1273294", // "Delhi"
            "1796236", // "Shanghai"
            "3451190", // "Rio de Janeiro",
            //"5368361", // "Los Angeles"
            //"3530597", // Mexico City
            //"6167865", // Toronto
            //"4887398", // Chicago
            //"4699066", // Houston
            //"3448439", // "Sao Paulo",
        };

        private static WeatherService _instance;

        public static WeatherService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WeatherService();

                return _instance;
            }
        }

        public async Task<CitiesWeatherRoot> GetWeatherAsync(List<string> cities, Units units = Units.Imperial)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(WeatherCitiesUri, string.Join(",", cities), units.ToString().ToLower());
                var json = await client.GetStringAsync(url);
                if (string.IsNullOrWhiteSpace(json))
                    return null;
                var result = DeserializeObject<CitiesWeatherRoot>(json);
                return result;
            }

        }
    }
}
