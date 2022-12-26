using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using WeatherApp.EXMPL.DATA;
using WeatherApp.EXMPL.GUI;
using WeatherApp.EXMPL.WINDOWS;
using City = WeatherApp.EXMPL.GUI.City;

namespace WeatherApp.EXMPL {
    public partial class MainWindow {
        private const string CityDataLocation    = "Cities.json";
        private const string WeatherDataLocation = "Weathers.json";
        
        public MainWindow() {
            InitializeComponent();
            
            CitiesInfo   = new List<CityInfo>();
            WeathersInfo = new List<WeatherInfo>();
            
            if (!File.Exists(CityDataLocation)) return;
            if (!File.Exists(WeatherDataLocation)) return;
            /*
            try {
                CitiesInfo   = JsonConvert.DeserializeObject<List<CityInfo>>(File.ReadAllText(CityDataLocation));
                WeathersInfo = JsonConvert.DeserializeObject<List<WeatherInfo>>(File.ReadAllText(WeatherDataLocation));
                UpdateInformation();
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
            */
        }
        
        public List<CityInfo> CitiesInfo { get; }
        public List<WeatherInfo> WeathersInfo { get; }
        
        public void ViewInfo(object sender, RoutedEventArgs routedEventArgs) {
            try {
                FullInfo.Children.Clear();
                var position = int.Parse((sender as Button)!.Name.Split("_")[1]);
                FullInfo.Children.Add(ExtendedCity.GetExtendedCityBody(CitiesInfo[position],
                    WeathersInfo[position]));
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
        public void DeleteCity(object sender, RoutedEventArgs routedEventArgs) {
            try {
                var position = int.Parse((sender as Button)!.Name.Split("_")[1]);
                CitiesInfo.RemoveAt(position);
                WeathersInfo.RemoveAt(position);
                UpdateInformation();
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
        public void UpdateInformation() {
            try {
                Cities.Children.Clear();
                for (var i = 0; i < CitiesInfo.Count; i++) {
                    Cities.Children.Add(City.GetCity(CitiesInfo[i], WeathersInfo[i], this));
                    (Cities.Children[^1] as Grid)!.Margin = new Thickness(0,i * 100,0,0);
                }
            }       
            catch (Exception exp)
            {
                MessageBox.Show($"введенно некоректное название города. {exp}");
            }
        }

        public AddCity addCity = null;

        private void AddCity(object sender, RoutedEventArgs e) {
            if (addCity != null) return;
            addCity = new WINDOWS.AddCity(this);
            addCity.Show();
        }
        
        private void SaveData(object sender, EventArgs e) {
            try {
                File.WriteAllText(CityDataLocation, JsonConvert.SerializeObject(CitiesInfo, Formatting.Indented));
                File.WriteAllText(WeatherDataLocation, JsonConvert.SerializeObject(WeathersInfo, Formatting.Indented));
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
    }
}