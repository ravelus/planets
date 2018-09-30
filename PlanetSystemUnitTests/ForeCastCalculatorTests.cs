using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PlanetSystem.Models;
using PlanetSystem.Forecast;

namespace PlanetSystemUnitTests
{
    [TestClass]
    public class ForeCastCalculatorTests
    {
        [TestMethod]
        public void ArePlanetsAlignedTest()
        {
            var system = new StarSystem
            {
                Star = new Planet(0, 0),
                Planets = new List<Planet>
                {
                    new Planet(90, 500),
                    new Planet(90, 2000),
                    new Planet(90, 1000)
                }
            };

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArePlanetsNotAlignedTest()
        {
            var system = new StarSystem
            {
                Star = new Planet(0, 0),
                Planets = new List<Planet>
                {
                    new Planet(40, 500),
                    new Planet(30, 2000),
                    new Planet(-90, 1000)
                }
            };

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ArePlanetsAlignedNegativeSpeedTest()
        {
            var system = new StarSystem
            {
                Star = new Planet(0, 0),
                Planets = new List<Planet>
                {
                    new Planet(90, 500),
                    new Planet(-270, 2000),
                    new Planet(-270, 1000)
                }
            };

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArePlanetsAndSunAligned()
        {
            var system = new StarSystem
            {
                Star = new Planet(0, 0),
                Planets = new List<Planet>
                {
                    new Planet(90, 500),
                    new Planet(90, 2000),
                    new Planet(90, 1000)
                }
            };

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAndSunAligned();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArePlanetsAndSunNotAligned()
        {
            var system = new StarSystem
            {
                Star = new Planet(0, 0),
                Planets = new List<Planet>
                {
                    new Planet(45, 500),
                    new Planet(90, 2000),
                    new Planet(90, 1000)
                }
            };

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAndSunAligned();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ArePlanetsAndSunAlignedNegativeSpeedTest()
        {
            var system = new StarSystem
            {
                Star = new Planet(0, 0),
                Planets = new List<Planet>
                {
                    new Planet(90, 500),
                    new Planet(-270, 2000),
                    new Planet(90, 1000)
                }
            };

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArePlanetsAlignedButNotWithSunTest()
        {
            var system = new StarSystem
            {
                Star = new Planet(0, 0),
                Planets = new List<Planet>
                {
                    new Planet(-1, 500),
                    new Planet(35, 1500),
                    new Planet(27, 1000)
                }
            };

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsTrue(result);

            result = calculator.ArePlanetsAndSunAligned();
            Assert.IsFalse(result);
        }
    }
}
