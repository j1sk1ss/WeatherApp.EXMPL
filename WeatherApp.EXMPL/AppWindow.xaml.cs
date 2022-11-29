using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using WeatherApp.EXMPL.DATA;
using WeatherApp.EXMPL.GUI;
using City = WeatherApp.EXMPL.GUI.City;

namespace WeatherApp.EXMPL {

    public partial class MainWindow : Window {
        private const string DataLocation = "Cities.json";
        
        public MainWindow() {
            InitializeComponent();
            UserCities = new Dictionary<CityInfo, WeatherInfo>();
            if (!File.Exists(DataLocation)) return;
            
            try {
                UserCities = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(DataLocation))!.UserCities;
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
        public Dictionary<CityInfo, WeatherInfo> UserCities { get; set; }
        private void AddCity(object sender, RoutedEventArgs e) {
            new WINDOWS.AddCity(this).Show();
        }
        public void ViewInfo(object sender, RoutedEventArgs routedEventArgs) {
            try {
                FullInfo.Children.Clear();
                var position = int.Parse((sender as Button)!.Name.Split("_")[1]);
                FullInfo.Children.Add(ExtendedCity.GetExtendedCityBody(UserCities.Keys.ToList()[position],
                    UserCities[UserCities.Keys.ToList()[position]]));
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
        public void UpdateInformation() {
            try {
                Cities.Children.Clear();
                for (var i = 0; i < UserCities.Count; i++) {
                    Cities.Children.Add(City.GetCity(UserCities.Keys.ToList()[i],
                        UserCities[UserCities.Keys.ToList()[i]], this));
                    (Cities.Children[^1] as Grid)!.Margin = new Thickness(0,i * 100,0,0);
                }
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }

        private void SaveData(object sender, EventArgs e) {
            try {
                var tempData = new UserData {
                    UserCities = UserCities
                };
                File.WriteAllText(DataLocation, JsonConvert.SerializeObject(tempData));
            }
            catch (Exception exception) {
                MessageBox.Show($"{exception}");
            }
        }
    }
}