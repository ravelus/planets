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

        double? _maxPerimeter;


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

        public bool IsStarContainedInPlanetsTriangle()
        {
            var starCoords = _system.Star.GetCartesianPosition();
            var planet1Coords = _system.Planets[0].GetCartesianPosition();
            var planet2Coords = _system.Planets[1].GetCartesianPosition();
            var planet3Coords = _system.Planets[2].GetCartesianPosition();

            var planetTriangleOrientation = CalculateTriangleOrientation(
                planet1Coords, planet2Coords, planet3Coords);

            var p1p2starTriangleOrientation = CalculateTriangleOrientation(
                planet1Coords, planet2Coords, starCoords);

            if (planetTriangleOrientation != p1p2starTriangleOrientation)
                return false;

            var p2p3starTriangleOrientation = CalculateTriangleOrientation(
                planet2Coords, planet3Coords, starCoords);

            if (planetTriangleOrientation != p2p3starTriangleOrientation)
                return false;

            var p3p1starTriangleOrientation = CalculateTriangleOrientation(
                planet3Coords, planet1Coords, starCoords);

            if (planetTriangleOrientation != p3p1starTriangleOrientation)
                return false;

            return true;
        }

        public bool HasTriangleMaxPerimeter()
        {

            var d1 = CalculateDistance(_system.Planets[0], _system.Planets[1]);
            var d2 = CalculateDistance(_system.Planets[1], _system.Planets[2]);
            var d3 = CalculateDistance(_system.Planets[0], _system.Planets[2]);

            var currentPerimeter = d1 + d2 + d3;

            return Math.Abs(GetMaxTrianglePerimeter() - currentPerimeter) <= TOLERANCE;
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

        // true -> positive orientation
        // false -> negative orientation
        bool CalculateTriangleOrientation(
            CartesianCoords a1, CartesianCoords a2, CartesianCoords a3)
        {
            //(A1.x - A3.x) * (A2.y - A3.y) - (A1.y - A3.y) * (A2.x - A3.x)
            var orientation =
                (a1.X - a3.X) * (a2.Y - a3.Y) - (a1.Y - a3.Y) * (a2.X - a3.X);

            return orientation > 0;
        }

        double GetMaxTrianglePerimeter()
        {
            if (_maxPerimeter == null)
            {
                _maxPerimeter = DoCalculateMaxTrianglePerimeter();
            }

            return _maxPerimeter.Value;
        }

        double DoCalculateMaxTrianglePerimeter()
        {
            // build a triangle with the given planets where they are as much
            // far away from each other as possible
            var planet1Reallocated = new Planet(0, _system.Planets[0].SunDistanceKm);
            var planet2Reallocated = new Planet(135, _system.Planets[1].SunDistanceKm);
            var planet3Reallocated = new Planet(225, _system.Planets[2].SunDistanceKm);

            // move the planets to the position I want to check!
            planet1Reallocated.Move();
            planet2Reallocated.Move();
            planet3Reallocated.Move();

            //calculate the distance they would have between them


            var d1 = CalculateDistance(planet1Reallocated, planet2Reallocated);
            var d2 = CalculateDistance(planet2Reallocated, planet3Reallocated);
            var d3 = CalculateDistance(planet1Reallocated, planet3Reallocated);

            return d1 + d2 + d3;
        }
    }
}
