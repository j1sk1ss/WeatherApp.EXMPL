using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WeatherApp.EXMPL.DATA;

namespace WeatherApp.EXMPL.GUI {
    public static class ExtendedCity {
        public static Grid GetExtendedCityBody(CityInfo cityInfo, WeatherInfo weatherInfo) { 
            var extendedBodyGrid = new Grid {
                Children = {
                    new Label {
                        FontSize = 30,
                        Content  = $"{cityInfo.name}",    
                        
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    new Label {
                        FontSize = 20,
                        Margin   = new Thickness(0,50,0,0),
                        Content  = $"Время: {DateTime.UtcNow.AddHours(weatherInfo.city.timezone / 3600f):HH:mm}",  
                        
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    new Label {
                        FontSize = 20,
                        Margin   = new Thickness(0,70,0,0),
                        Content  = $"Температура: {Math.Round(weatherInfo.list[0].main.temp, 0)}°",  
                        
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    new Label {
                        FontSize = 15,
                        Margin   = new Thickness(0,100,0,0),
                        Content  = $"Скорость ветра: {weatherInfo.list[0].wind.speed} м/c" + 
                                   $"\nТемпература по ощущению: {Math.Round(weatherInfo.list[0].main.feels_like, 0)}°",  
                        
                        HorizontalAlignment = HorizontalAlignment.Left,
                    },
                }
            };

            var daysGrid = new Grid {
                Height = 100,
                VerticalAlignment = VerticalAlignment.Bottom,
                Children = {
                    new Line {
                        X1 = 0,
                        X2 = 550,
                        Y1 = 0,
                        Y2 = 0,
                        Stroke = Brushes.Black
                    },
                    new Line {
                        X1 = 0,
                        X2 = 550,
                        Y1 = 100,
                        Y2 = 100,
                        Stroke = Brushes.Black
                    }
                }
            };

            const int indent = 80;
            
            for (var i = 0; i < weatherInfo.list.Count; i++) {
                var day = Day.GetDay(weatherInfo, i);
                daysGrid.Children.Add(day);
                (daysGrid.Children[^1] as Grid)!.Margin = new Thickness(i * indent,0,0,0);
            }
            
            extendedBodyGrid.Children.Add(daysGrid);
            return extendedBodyGrid;
        }
    }
}