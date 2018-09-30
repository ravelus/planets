using System;
using System.Linq;
using System.Collections.Generic;

using PlanetSystem.Models;

namespace PlanetSystem.Forecast
{
    public class ForecastCalculator
    {
        public const double TOLERANCE = 0.02;

        readonly StarSystem _system;

        IDictionary<double, Tuple<Planet, Planet>> _distances;


        public ForecastCalculator(StarSystem sys)
        {
            _system = sys;
        }



        public bool ArePlanetsAligned()
        {
            CalculateDistances();

            var maxDistance = _distances.Keys.Max();

            _distances.Remove(maxDistance);

            return (maxDistance - _distances.Keys.Sum()) < TOLERANCE;
        }

        public bool ArePlanetsAndSunAligned()
        {
            //TODO
            return false;
        }

        void CalculateDistances()
        {
            // distances already cached
            if (_distances != null)
                return;

            _distances = new Dictionary<double, Tuple<Planet, Planet>>();
            foreach (var planet1 in _system.Planets)
            {
                foreach (var planet2 in _system.Planets)
                {
                    if (planet1 == planet2)
                        continue;

                    _distances.Add(
                        CalculateDistance(planet1, planet2),
                        new Tuple<Planet, Planet>(planet1, planet2));
                }
            }
        }

        public void CleanDistances()
        {
            _distances = null;
        }

        double CalculateDistance(Planet p1, Planet p2)
        {
            var p1Coords = p1.GetCartesianPosition();
            var p2Coords = p2.GetCartesianPosition();

            return Math.Sqrt(
                Math.Pow(Math.Abs(p1Coords.X - p2Coords.X), 2) +
                Math.Pow(Math.Abs(p1Coords.Y - p2Coords.Y), 2));
        }
    }
}
