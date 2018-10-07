using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetSystem.Models
{
    public struct StarSystem
    {
        public Planet Star { get; private set; }

        public IList<Planet> Planets { get; private set; }

        public StarSystem(Planet star, IList<Planet> planets)
        {
            if (planets.Count != 3)
                throw new ArgumentException("The number of planets must be 3");

            var coords = star.GetCartesianPosition();
            if (coords.X != 0 || coords.Y != 0)
                throw new ArgumentException("The star must be located in the origin");

            Star = star;
            Planets = planets;
        }

        public void TranslateStep()
        {
            Parallel.ForEach(Planets, p => p.Move());
        }
    }
}
