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


        public ForecastCalculator(StarSystem sys)
        {
            _system = sys;
        }

        public bool ArePlanetsAligned()
        {
            var distances = CalculateDistances(_system.Planets);

            var maxDistance = distances.Max();

            distances.Remove(maxDistance);

            return Math.Abs(maxDistance - distances.Sum()) < TOLERANCE;
        }

        public bool ArePlanetsAndSunAligned()
        {
            if (!ArePlanetsAligned())
                return false;

            var items = new List<Planet>();
            items.Add(_system.Star);

            for (int i = 1; i < _system.Planets.Count; i++)
            {
                items.Add(_system.Planets[i]);
            }

            var distances = CalculateDistances(items);

            var maxDistance = distances.Max();

            distances.Remove(maxDistance);

            return (Math.Abs(maxDistance - distances.Sum()) < TOLERANCE);
        }

        IList<double> CalculateDistances(IList<Planet> planets)
        {
            var result  = new List<double>();
            for (int i = 0; i < planets.Count; i++)
            {
                for (int j = i + 1; j < planets.Count; j++)
                {
                    result.Add(CalculateDistance(planets[i], planets[j]));
                }
            }

            return result;
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
