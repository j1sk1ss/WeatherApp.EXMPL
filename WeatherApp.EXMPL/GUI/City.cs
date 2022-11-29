using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeatherApp.EXMPL.DATA;

namespace WeatherApp.EXMPL.GUI {
    public static class City {
        public static Grid GetCity(CityInfo cityInfo, WeatherInfo weatherInfo, MainWindow mainWindow) { // Gets info from api class, and gets main form
            var cityGrid = new Grid {
                VerticalAlignment = VerticalAlignment.Top,
                Height = 100,
                Children = {
                    new Line {
                        X1 = 0,
                        X2 = 250,
                        Y1 = 0,
                        Y2 = 0,
                        Stroke = Brushes.Black
                    },
                    new Line {
                        X1 = 0,
                        X2 = 250,
                        Y1 = 100,
                        Y2 = 100,
                        Stroke = Brushes.Black
                    },
                    new Label {
                        FontSize = 20,
                        Content  = cityInfo.name // from api class
                    },
                    new Label {
                        FontSize = 15,
                        Margin   = new Thickness(0,50,0,0),
                        Content  = Math.Round(weatherInfo.list[0].main.temp_max, 0) + "°" // from api class
                    },
                    new Label {
                        FontSize = 14,
                        Margin   = new Thickness(0,70,0,0),
                        Content  = Math.Round(weatherInfo.list[0].main.temp_min, 0) + "°" // from api class
                    },
                    new Image {
                        Height  = 50,
                        Width   = 50,
                        Stretch = Stretch.Fill,
                        Margin  = new Thickness(100,0,0,50),
                        Source  = new BitmapImage(new Uri($"IMG/ICONS/{weatherInfo.list[0].weather[0].icon}@2x.png",
                            UriKind.Relative)) // from api class
                    }
                }
            };
            var extendButton = new Button {
                Width = 25,
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = ">",
                Name = "button_" + mainWindow.Cities.Children.Count
            };
            extendButton.Click += mainWindow.ViewInfo;
            cityGrid.Children.Add(extendButton);
            return cityGrid;
        }
    }
}