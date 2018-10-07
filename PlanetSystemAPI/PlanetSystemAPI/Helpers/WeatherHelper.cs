using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace PlanetSystemAPI.Helpers
{
    public static class WeatherHelper
    {
        static IDictionary<int, string> _weatherData;
        static object _syncAccessObj = new object();
        static object _syncLoadObj = new object();

        public static string GetWeatherOfDay(int day)
        {
            lock (_syncAccessObj)
            {
                if (_weatherData == null)
                {
                    lock (_syncLoadObj)
                    {
                        LoadWeatherData();
                    }
                }
            }

            if (day > _weatherData.Count)
                throw new ArgumentException("Sorry, but I didn't processed so many days!");

            return TranslateWeatherIntoSpanish(_weatherData[day]);
        }

        static void LoadWeatherData()
        {
            _weatherData = new Dictionary<int, string>();
            string dataPath = @"C:\Users\lurodrig\Desktop\personal\codesamples\mercadolibre-planets\Planets\WeatherSimulator\bin\Debug\netcoreapp2.1\data.txt";

            var forecastByDay = File.ReadLines(dataPath);

            char separator = ':';
            forecastByDay
                .ToList()
                .ForEach(line =>
                    _weatherData.Add(int.Parse(line.Split(separator)[0]), line.Split(separator)[1]));
        }

        static string TranslateWeatherIntoSpanish(string weather)
        {
            switch (weather)
            {
                case "None" : return "normal";
                case "Dry" : return "seco";
                case "Wet": return "lluvia";
                case "Very wet": return "mucha lluvia";
                case "Great": return "ideal";
                default:
                    throw new ArgumentException("Unexpected weather. Data corrupt?");
            }
        }
    }
}