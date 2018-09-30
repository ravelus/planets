using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PlanetSystem;

namespace UnitTestProject1
{
    [TestClass]
    public class PlanetTests
    {
        [TestMethod]
        public void MovePositiveSpeedTest()
        {
            var somePlanet = new Planet(10, 5000);

            somePlanet.Move();

            var currentRadius = somePlanet.Radius;

            Assert.AreEqual(10, (int)currentRadius);
        }

        [TestMethod]
        public void MovePositiveSpeedMaxDegreesTest()
        {
            var somePlanet = new Planet(350, 5000);

            somePlanet.Move();
            somePlanet.Move();

            var currentRadius = somePlanet.Radius;

            Assert.AreEqual(340, (int)currentRadius);
        }

        [TestMethod]
        public void MoveNegativeSpeedTest()
        {
            var somePlanet = new Planet(-10, 5000);

            somePlanet.Move();

            var currentRadius = somePlanet.Radius;

            Assert.AreEqual(350, (int)currentRadius);
        }

        [TestMethod]
        public void MoveNegativeSpeedMaxDegreesTest()
        {
            var somePlanet = new Planet(-300, 5000);

            somePlanet.Move();
            somePlanet.Move();

            var currentRadius = somePlanet.Radius;

            Assert.AreEqual(120, (int)currentRadius);
        }

        [TestMethod]
        public void GetPositionPositiveSpeedTest()
        {
            var somePlanet = new Planet(10, 500);
            somePlanet.Move();

            var actualPosition = somePlanet.GetCartesianPosition();
            var xRounded = Math.Round(actualPosition.X, 2);
            var yRounded = Math.Round(actualPosition.Y, 2);

            Assert.AreEqual(492.40, xRounded);
            Assert.AreEqual(86.82, yRounded);
        }
    }
}
