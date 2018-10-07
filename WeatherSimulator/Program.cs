using System.Collections.Generic;
using System.Threading.Tasks;

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
            int dryDays = 0;
            int wetDays = 0;
            int perfectDays = 0;
            IList<int> veryWetDaysRegister = new List<int>();

            WeatherForecast forecaster = new WeatherForecast(system);
            for (int i = 1; i <= totalDays; i++) //let's start couting on day 1 :)
            {
                forecaster.Dawn();

                if (forecaster.IsGreatToday())
                {
                    perfectDays++;
                    continue;
                }

                if (forecaster.IsDryToday())
                {
                    dryDays++;
                    continue;
                }

                if (!forecaster.IsWetToday())
                    continue;

                wetDays++;

                if (forecaster.IsVeryWetToday())
                {
                    veryWetDaysRegister.Add(i);
                }
            }
        }
    }
}
