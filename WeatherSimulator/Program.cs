using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            Stopwatch timer = new Stopwatch();
            timer.Start();
            var forecastByDay = Run(system, totalDays);
            timer.Stop();

            SaveForecast(forecastByDay);

            PrintSummary(forecastByDay);

            Console.WriteLine($"Total time processing: {timer.ElapsedMilliseconds} millis");

            if (timer.ElapsedMilliseconds > 1000)
                Console.WriteLine("Expected to be under 1 sec!!!");

            Console.ReadLine();
        }

        static Dictionary<int, WeatherDescription> Run(StarSystem system, int totalDays)
        {
            var result = new Dictionary<int, WeatherDescription>();

            WeatherForecast forecaster = new WeatherForecast(system);
            for (int i = 1; i <= totalDays; i++) //let's start couting on day 1 :)
            {
                forecaster.Dawn();

                result.Add(i, WeatherDescription.None);

                if (forecaster.IsGreatToday())
                {
                    result[i] = WeatherDescription.Great;
                    continue;
                }

                if (forecaster.IsDryToday())
                {
                    result[i] = WeatherDescription.Dry;
                    continue;
                }

                if (!forecaster.IsWetToday())
                    continue;

                result[i] = WeatherDescription.Wet;

                if (forecaster.IsVeryWetToday())
                {
                    result[i] = WeatherDescription.VeryWet;
                }
            }

            return result;
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
