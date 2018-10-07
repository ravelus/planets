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

        internal bool IsStarContainedInPlanetsTriangle()
        {
            var planet1Coords = _system.Planets[0].GetCartesianPosition();
            var planet2Coords = _system.Planets[1].GetCartesianPosition();
            var planet3Coords = _system.Planets[2].GetCartesianPosition();

            // cartesian product of coords, math definition; see:
            // http://funes.uniandes.edu.co/8137/1/pag2.html
            var a1 = 0 - 
                ((planet1Coords.X * planet3Coords.Y) - (planet1Coords.Y * planet3Coords.X) /
                (planet2Coords.X * planet3Coords.Y) - (planet2Coords.Y * planet3Coords.X));

            if (a1 <= 0)
                return false;

            // cartesian product of coords, math definition; see:
            // http://funes.uniandes.edu.co/8137/1/pag2.html
            var a2 = 0 -
                ((planet1Coords.X * planet2Coords.Y) - (planet1Coords.Y * planet2Coords.X) /
                (planet2Coords.X * planet3Coords.Y) - (planet2Coords.Y * planet3Coords.X));

            if (a2 <= 0)
                return false;

            return true;
        }

        internal bool IsTriangleMaxPerimeter()
        {
            throw new NotImplementedException();
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
