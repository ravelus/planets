using System;

namespace PlanetSystem.Models
{
    public class Planet
    {
        const uint MAX_DEGREES = 360;

        readonly int _speed;

        public uint Radius { get; private set; }
        public uint SunDistanceKm { get; private set; }

        public Planet(int speed, uint sunDistanceKm)
        {
            _speed = speed;
            SunDistanceKm = sunDistanceKm;

            if (_speed > 0)
                Radius = 0;
            else
                Radius = MAX_DEGREES;
        }


        public CartesianCoords GetCartesianPosition()
        {
            return new CartesianCoords
            {
                X = Math.Cos(RadiusToRadians) * SunDistanceKm,
                Y = Math.Sin(RadiusToRadians) * SunDistanceKm
            };
        }

        public void Move()
        {
            var nextPosition = Radius + _speed;

            if (nextPosition < 0)
            {
                nextPosition += MAX_DEGREES;
            }

            Radius = (uint)nextPosition % MAX_DEGREES;
        }

        double RadiusToRadians
        {
            get
            {
                const int PI_RADIANS = 180;

                return Radius * Math.PI / PI_RADIANS;
            }
        }
    }
}
