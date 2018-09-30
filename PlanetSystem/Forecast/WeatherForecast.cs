using System.Threading.Tasks;

using PlanetSystem.Models;

namespace PlanetSystem.Forecast
{
    public class WeatherForecast
    {
        ForecastCalculator _calculator;
        StarSystem _system;

        WeatherForecast(StarSystem sys)
        {
            _system = sys;
            _calculator = new ForecastCalculator(sys);
        }

        public void Dawn()
        {
            _system.TranslateStep();
            _calculator.CleanDistances();
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
            return _calculator.ArePlanetsAligned() &&
                !_calculator.ArePlanetsAndSunAligned();
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
