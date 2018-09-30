using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetSystem.Models
{
    public struct StarSystem
    {
        public Planet Star { get; set; }

        public IList<Planet> Planets { get; set; }

        public void TranslateStep()
        {
            Parallel.ForEach(Planets, p => p.Move());
        }
    }
}
