using System.Collections.Generic;

using PlanetSystem.Forecast;
using PlanetSystem.Models;


namespace WeatherSimulator
{
    static class Program
    {
        static void Main(string[] args)
        {
            int totalDays = 10 * 365; // 10 years on the Earth

            int dryDays = 0;
            int wetDays = 0;
            int perfectDays = 0;
            IList<int> veryWetDaysRegister = new List<int>();

            Planet ferengi = new Planet(1, 500);
            Planet betasoide = new Planet(3, 2000);
            Planet vulcano = new Planet(5, 1000);
            Planet star = new Planet(0, 0);

            StarSystem system = new StarSystem
            {
                Star = star,
                Planets = new List<Planet>
                {
                    ferengi,
                    betasoide,
                    vulcano
                }
            };

            WeatherForecast forecaster = new WeatherForecast(system);
            for (int i = 1; i <= totalDays; i++) //let's start couting on day 1 :)
            {
                forecaster.Dawn();

                if (forecaster.IsGreatToday())
                {
                    perfectDays++;
                }

                if (forecaster.IsDryToday())
                {
                    dryDays++;
                }

                if (forecaster.IsWetToday())
                {
                    wetDays++;

                    if (forecaster.IsVeryWetToday())
                    {
                        veryWetDaysRegister.Add(i);
                    }
                }
            }
        }
    }
}
