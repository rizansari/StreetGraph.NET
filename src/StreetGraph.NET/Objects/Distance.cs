using System;
using System.Collections.Generic;
using System.Text;

namespace StreetGraph.NET.Objects
{
    public class Distance
    {
        private double _distance;
        private DistanceUnit _unit;

        public Distance(double distance, DistanceUnit unit)
        {
            _distance = distance;
            _unit = unit;
        }

        public override string ToString()
        {
            return GetDistance(DistanceUnit.Meters).ToString();
        }

        public double GetDistance(DistanceUnit unit)
        {
            switch(unit)
            {
                case DistanceUnit.Kilometers:
                    return _getDistanceInKilometers();

                case DistanceUnit.Meters:
                    return _getDistanceInMeters();

                case DistanceUnit.Miles:
                    return _getDistanceInMiles();

                default:
                    return 0;
            }
        }

        private double _getDistanceInKilometers()
        {
            if (_unit == DistanceUnit.Kilometers)
            {
                return _distance;
            }
            else if (_unit == DistanceUnit.Meters)
            {
                return _distance / 1000.0;
            }
            else if (_unit == DistanceUnit.Miles)
            {
                return _distance * 1.609;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private double _getDistanceInMeters()
        {
            if (_unit == DistanceUnit.Kilometers)
            {
                return _distance * 1000.0;
            }
            else if (_unit == DistanceUnit.Meters)
            {
                return _distance;
            }
            else if (_unit == DistanceUnit.Miles)
            {
                return _distance * 1609;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private double _getDistanceInMiles()
        {
            if (_unit == DistanceUnit.Kilometers)
            {
                return _distance / 1.609;
            }
            else if (_unit == DistanceUnit.Meters)
            {
                return _distance / 1609.0;
            }
            else if (_unit == DistanceUnit.Miles)
            {
                return _distance;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
