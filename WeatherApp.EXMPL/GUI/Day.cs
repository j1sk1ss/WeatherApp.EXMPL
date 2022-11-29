using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeatherApp.EXMPL.DATA;

namespace WeatherApp.EXMPL.GUI {
    public static class Day {
        public static Grid GetDay(WeatherInfo weatherInfo, int position) { // Gets info from api
            var dayGrid = new Grid {
                Width = 80,
                HorizontalAlignment = HorizontalAlignment.Left,
                Children = {
                    new Line {
                        X1 = 0,
                        X2 = 0,
                        Y1 = 0,
                        Y2 = 100,
                        Stroke = Brushes.Black
                    },
                    new Line {
                        X1 = 80,
                        X2 = 80,
                        Y1 = 0,
                        Y2 = 100,
                        Stroke = Brushes.Black
                    },
                    new Label {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Content  = ((position + (int)DateTime.Now.DayOfWeek) % 7) switch {
                            0 => "Воскресенье",
                            1 => "Понедельник",
                            2 => "Вторник",
                            3 => "Среда",
                            4 => "Четверг",
                            5 => "Пятница",
                            6 => "Суббота",
                            _ => "NULL"
                        }
                    },
                    new Label {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontSize = 20,
                        Margin   = new Thickness(0,30,0,0),
                        Content  = Math.Round(weatherInfo.list[position].main.temp, 0) // from api class
                    },
                    new Label {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontSize = 10,
                        Margin   = new Thickness(40,20,0,0),
                        Content  = Math.Round(weatherInfo.list[position].main.temp_min, 0) // from api class
                    },
                    new Image {
                        Height  = 50,
                        Width   = 50,
                        Stretch = Stretch.Fill,
                        Margin  = new Thickness(0,55,0,0),
                        Source  = new BitmapImage(new Uri($"IMG/ICONS/{weatherInfo.list[position].weather[0].icon}@2x.png",
                            UriKind.Relative)) // from api class
                    }
                }
            };
            return dayGrid;
        }
    }
}