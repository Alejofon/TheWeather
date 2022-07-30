using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TheWeather.Models;

namespace TheWeather.ViewModels
{
    
    public class TheWeatherViewModel
    {
        public string city { get; set; }
        public string country { get; set; }

        private async void GetData(string city, string country)
        {
            city = city.Trim();
            country = country.Trim();
            var cliente = new HttpClient();
            var resultado = await cliente.GetStringAsync($"https://api.weatherbit.io/v2.0/current?city={city}&country={country}&key=f73c9b3c5b7547a7a299daca0fdcc6e0");
            var Weather = Temperatures.FromJson(resultado);
        }



    }

   
}
