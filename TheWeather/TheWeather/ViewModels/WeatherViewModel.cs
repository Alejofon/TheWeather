using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheWeather.Models;
using Xamarin.Forms;

namespace TheWeather.ViewModels
{

    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherData data;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WeatherData Data
        {
            get => data; set
            {
                data = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchCommand { get; set; }

        public WeatherViewModel()
        {
            SearchCommand = new Command(async (searchTerm) =>
            {
                var entrada = searchTerm as string;
                var datos = entrada.Split(',');
                var lat = datos[0];
                var lon = datos[1];
                await GetData($"https://api.weatherbit.io/v2.0/current?lat={lat}&lon={lon}&lang=es&key=f73c9b3c5b7547a7a299daca0fdcc6e0");
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
