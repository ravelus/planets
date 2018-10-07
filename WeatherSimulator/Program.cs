using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using PlanetSystem.Forecast;
using PlanetSystem.Models;

namespace WeatherSimulator
{
    static class Program
    {
        static void Main(string[] args)
        {
            Planet ferengi = new Planet(1, 500);
            Planet betasoide = new Planet(3, 2000);
            Planet vulcano = new Planet(5, 1000);
            Planet star = new Planet(0, 0);

            var planets = new List<Planet>
            {
                ferengi,
                betasoide,
                vulcano
            };

            StarSystem system = new StarSystem(star, planets);

            int totalDays = 10 * 365; // 10 years on the Earth
            RunAsync(system, totalDays);
        }

        static void RunAsync(StarSystem system, int totalDays)
        {
            var forecastByDay = new Dictionary<int, WeatherDescription>();

            WeatherForecast forecaster = new WeatherForecast(system);
            for (int i = 1; i <= totalDays; i++) //let's start couting on day 1 :)
            {
                forecaster.Dawn();

                forecastByDay.Add(i, WeatherDescription.None);

                if (forecaster.IsGreatToday())
                {
                    forecastByDay[i] = WeatherDescription.Great;
                    continue;
                }

                if (forecaster.IsDryToday())
                {
                    forecastByDay[i] = WeatherDescription.Dry;
                    continue;
                }

                if (!forecaster.IsWetToday())
                    continue;

                forecastByDay[i] = WeatherDescription.Wet;

                if (forecaster.IsVeryWetToday())
                {
                    forecastByDay[i] = WeatherDescription.VeryWet;
                }
            }

            SaveForecast(forecastByDay);

            PrintSummary(forecastByDay);

            Console.ReadLine();
        }

        static void SaveForecast(Dictionary<int, WeatherDescription> forecastByDay)
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dataPath = Path.Combine(assemblyLocation, "data.txt");

            var weatherData = forecastByDay.Select(day => $"{day.Key}:{day.Value}");
            File.WriteAllText(dataPath, string.Join(Environment.NewLine, weatherData));
        }

        static void PrintSummary(Dictionary<int, WeatherDescription> forecastByDay)
        {
            Console.WriteLine($"Days with dry weather: " +
                $"{forecastByDay.Count(days => days.Value == WeatherDescription.Dry)}");

            Console.WriteLine($"Days with wet weather: " +
                $"{forecastByDay.Count(days => days.Value == WeatherDescription.Wet)}");

            Console.WriteLine($"Days with great weather: " +
                $"{forecastByDay.Count(days => days.Value == WeatherDescription.Great)}");

            Console.WriteLine($"Days with very wet weather: " +
                $"{forecastByDay.Count(days => days.Value == WeatherDescription.VeryWet)}");

            Console.WriteLine($"Days with regular weather: " +
                $"{forecastByDay.Count(days => days.Value == WeatherDescription.None)}");
        }
    }
}
