using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using WeatherApp.EXMPL.DATA;

namespace WeatherApp.EXMPL.WINDOWS {
    public partial class AddCity : Window {
        private const string ApiKey = "d3e8c8e06baab4f0fabcbee86a29c51b";
        public AddCity(MainWindow mainWindow) {
            InitializeComponent();
            MainWindow = mainWindow;
        }
        private MainWindow MainWindow { get; set; }
        private async void SendCityInfo(object sender, RoutedEventArgs e) {
            try {
                var locationRequest    = 
                    $"https://api.openweathermap.org/geo/1.0/direct?q={CityName.Text}&limit=2&appid={ApiKey}";
                var locationWebRequest = WebRequest.Create(locationRequest);
                var locationResponse   = await locationWebRequest.GetResponseAsync();
                
                string location;
                
                using (var stream = locationResponse.GetResponseStream()) {
                    using (var reader = new StreamReader(stream!)) {
                        location = await reader.ReadToEndAsync();
                    };
                };
                
                locationResponse.Close();
                
                var locationData = JsonConvert.DeserializeObject<List<CityInfo>>(location)![0];
                var weatherRequest =
                    $"https://api.openweathermap.org/data/2.5/forecast?lat={locationData.lat}" +
                    $"&lon={locationData.lon}&appid={ApiKey}&units=metric";     
                
                var weatherWebRequest  = WebRequest.Create(weatherRequest);                
                var weatherResponse    = await weatherWebRequest.GetResponseAsync();    
                
                string weather;     
                
                using (var stream = weatherResponse.GetResponseStream()) {
                    using (var reader = new StreamReader(stream!)) {
                        weather = await reader.ReadToEndAsync();
                    };
                };
                
                weatherResponse.Close();
                
                MainWindow.UserCities.Add(locationData, JsonConvert.DeserializeObject<WeatherInfo>(weather));
                MainWindow.UpdateInformation();
                Close();
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
    }
}