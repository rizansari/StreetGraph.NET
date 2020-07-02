using System;
using System.Collections.Generic;
using System.Text;

namespace StreetGraph.NET.Objects
{
    public class Speed
    {
        private double _speed;
        private SpeedUnit _unit;

        public Speed(double speed, SpeedUnit unit)
        {
            _speed = speed;
            _unit = unit;
        }

        public override string ToString()
        {
            return GetSpeed(SpeedUnit.KilometersPerHour).ToString();
        }

        public double GetSpeed(SpeedUnit unit)
        {
            switch (unit)
            {
                case SpeedUnit.KilometersPerHour:
                    return _getSpeedInKilometersPerHour();

                case SpeedUnit.MetersPerSecond:
                    return _getSpeedInMetersPerSecond();

                case SpeedUnit.MilesPerHour:
                    return _getSpeedInMilesPerHour();

                default:
                    return 0;
            }
        }

        private double _getSpeedInKilometersPerHour()
        {
            if (_unit == SpeedUnit.KilometersPerHour)
            {
                return _speed;
            }
            else if (_unit == SpeedUnit.MetersPerSecond)
            {
                return _speed * 3.6;
            }
            else if (_unit == SpeedUnit.MilesPerHour)
            {
                return _speed * 1.609;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private double _getSpeedInMetersPerSecond()
        {
            if (_unit == SpeedUnit.KilometersPerHour)
            {
                return _speed / 3.6;
            }
            else if (_unit == SpeedUnit.MetersPerSecond)
            {
                return _speed;
            }
            else if (_unit == SpeedUnit.MilesPerHour)
            {
                return _speed / 2.237;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private double _getSpeedInMilesPerHour()
        {
            if (_unit == SpeedUnit.KilometersPerHour)
            {
                return _speed / 1.609;
            }
            else if (_unit == SpeedUnit.MetersPerSecond)
            {
                return _speed * 2.237;
            }
            else if (_unit == SpeedUnit.MilesPerHour)
            {
                return _speed;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
