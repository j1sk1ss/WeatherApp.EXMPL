using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeatherApp.EXMPL.DATA;

namespace WeatherApp.EXMPL.GUI {
    public static class City {
        public static Grid GetCity(CityInfo cityInfo, WeatherInfo weatherInfo, MainWindow mainWindow) {
            try
            {
                var cityGrid = new Grid()
                {
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
                            Content  = cityInfo.name,
                            Foreground = Brushes.White

                        },
                        new Label {
                            FontSize = 15,
                            Margin   = new Thickness(0,50,0,0),
                            Content  = Math.Round(weatherInfo.list[0].main.temp_max, 0) + "°",
                            Foreground = Brushes.White
                        },
                        new Label {
                            FontSize = 14,
                            Margin   = new Thickness(0,70,0,0),
                            Content  = Math.Round(weatherInfo.list[0].main.temp_min, 0) + "°",
                            Foreground = Brushes.White
                        },
                        new Image {
                            Height  = 50,
                            Width   = 50,
                            Stretch = Stretch.Fill,
                            Margin  = new Thickness(100,0,0,50),
                            Source  = new BitmapImage(new Uri($"IMG/ICONS/{weatherInfo.list[0].weather[0].icon ?? "01"}@2x.png",
                                UriKind.Relative))
                        }
                    }
                };
                var extendButton = new Button
                {
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Width = 25,
                    Content = ">",
                    Name = "button_" + mainWindow.Cities.Children.Count
                };
                extendButton.Click += mainWindow.ViewInfo;
                cityGrid.Children.Add(extendButton);

                extendButton = new Button
                {
                    Width = 25,
                    Height = 25,
                    Margin = new Thickness(35, 0, 0, 0),

                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,

                    Content = "-",
                    Name = "delete_" + mainWindow.Cities.Children.Count
                };
                extendButton.Click += mainWindow.DeleteCity;
                cityGrid.Children.Add(extendButton);

                return cityGrid;
            } catch (Exception ex)
            {
                MessageBox.Show($"{ex}");
            }
            return null;
            
        }
    }
}