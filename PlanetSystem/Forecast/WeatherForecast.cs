using PlanetSystem.Models;

namespace PlanetSystem.Forecast
{
    public class WeatherForecast
    {
        readonly ForecastCalculator _calculator;
        readonly StarSystem _system;

        public WeatherForecast(StarSystem sys)
        {
            _system = sys;
            _calculator = new ForecastCalculator(sys);
        }

        public void Dawn()
        {
            _system.TranslateStep();
        }

        public bool IsDryToday()
        {
            return _calculator.ArePlanetsAndSunAligned();
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
            return
                _calculator.ArePlanetsAligned() &&
                !_calculator.ArePlanetsAndSunAligned();
        }
    }
}
