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
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(90, 500),
                new Planet(90, 2000),
                new Planet(90, 1000)
            };
            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArePlanetsNotAlignedTest()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(40, 500),
                new Planet(30, 2000),
                new Planet(-90, 1000)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ArePlanetsAlignedNegativeSpeedTest()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(90, 500),
                new Planet(-270, 2000),
                new Planet(-270, 1000)
            };
            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArePlanetsAndSunAligned()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(90, 500),
                new Planet(90, 2000),
                new Planet(90, 1000)
            };
            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAndSunAligned();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArePlanetsAndSunNotAligned()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(45, 500),
                new Planet(90, 2000),
                new Planet(90, 1000)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAndSunAligned();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ArePlanetsAndSunAlignedNegativeSpeedTest()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(90, 500),
                new Planet(-270, 2000),
                new Planet(90, 1000)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ArePlanetsAlignedButNotWithSunTest()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(-1, 500),
                new Planet(35, 1500),
                new Planet(27, 1000)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.ArePlanetsAligned();

            Assert.IsTrue(result);

            result = calculator.ArePlanetsAndSunAligned();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StarContainedInTrianglePositiveOrientationTest()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(90, 500),
                new Planet(225, 1000),
                new Planet(300, 1500)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.IsStarContainedInPlanetsTriangle();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StarContainedInTriangleNegativeOrientationTest()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(270, 500),
                new Planet(135, 1000),
                new Planet(60, 1500)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.IsStarContainedInPlanetsTriangle();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StarNotContainedInTrianglePositiveOrientationTest()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(90, 500),
                new Planet(135, 1000),
                new Planet(45, 1500)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.IsStarContainedInPlanetsTriangle();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StarNotContainedInTriangleNegativeOrientationTest()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(180, 500),
                new Planet(135, 1000),
                new Planet(225, 1500)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.IsStarContainedInPlanetsTriangle();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TriangleHasMaxPerimeterTest()
        {
            var star = new Planet(0, 0);
            var planets = new List<Planet>
            {
                new Planet(90, 500),
                new Planet(225, 1000),
                new Planet(315, 1500)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.HasTriangleMaxPerimeter();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TriangleHasMaxPerimeterCase2Test()
        {
            var star = new Planet(0, 0);

            // a "not-so-perfect" case
            var planets = new List<Planet>
            {
                new Planet(90 + 10, 500),
                new Planet(225 + 10, 500),
                new Planet(315 + 10, 500)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.HasTriangleMaxPerimeter();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TriangleDoesNotHaveMaxPerimeterTest()
        {
            var star = new Planet(0, 0);

            // a "not-so-perfect" case
            var planets = new List<Planet>
            {
                new Planet(0, 500),
                new Planet(90, 1000),
                new Planet(225, 1500)
            };

            var system = new StarSystem(star, planets);

            system.TranslateStep();

            var calculator = new ForecastCalculator(system);
            bool result = calculator.HasTriangleMaxPerimeter();

            Assert.IsFalse(result);
        }
    }
}
