using PlanetSystem.Models;

namespace PlanetSystem.Forecast
{
    public class WeatherForecast
    {
        Planet _planet1;
        Planet _planet2;
        Planet _planet3;

        WeatherForecast(Planet planet1, Planet planet2, Planet planet3)
        {
            _planet1 = planet1;
            _planet2 = planet2;
            _planet3 = planet3;
        }

        public void Dawn()
        {
            _planet1.Move();
            _planet2.Move();
            _planet3.Move();
        }

        public bool IsDryToday()
        {
            return ArePlanetsAndSunAligned();
        }

        public bool IsWetToday()
        {
            //TODO
            return false;
        }

        public bool IsVeryWetToday()
        {
            //TODO
            return false;
        }

        public bool IsGreatToday()
        {
            return ArePlanetsAligned() && !ArePlanetsAndSunAligned();
        }

        bool ArePlanetsAligned()
        {
            //TODO
            return false;
        }

        bool ArePlanetsAndSunAligned()
        {
            //TODO
            return false;
        }
    }
}
