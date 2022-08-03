using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheWeather.Models;
using Xamarin.Forms;

namespace TheWeather.ViewModels
{

    public class WeatherViewModel
    {
               
        public WeatherData Data { get; set; } 
        public ICommand SearchCommand { get; set; }       
        
        public WeatherViewModel()
        {
            SearchCommand = new Command(async (searchTerm) =>
            {
                await GetData("https://api.weatherbit.io/v2.0/current?lat=4.674303777609851&lon=-74.11364348674834&key=f73c9b3c5b7547a7a299daca0fdcc6e0");
            });
        }

        private async Task GetData(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); 
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeatherData>(json);
            Data = result;
        }
    }




   
}
