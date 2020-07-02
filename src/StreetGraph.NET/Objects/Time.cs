using System;
using System.Collections.Generic;
using System.Text;

namespace StreetGraph.NET.Objects
{
    public class Time
    {
        private double _time;
        private TimeUnit _unit;

        public Time(double time, TimeUnit unit)
        {
            _time = time;
            _unit = unit;
        }

        public override string ToString()
        {
            return GetTime(TimeUnit.Second).ToString();
        }

        public double GetTime(TimeUnit unit)
        {
            switch (unit)
            {
                case TimeUnit.Hour:
                    return _getTimeInHour();

                case TimeUnit.Minute:
                    return _getTimeInMinute();

                case TimeUnit.Second:
                    return _getTimeInSecond();

                case TimeUnit.Millisecond:
                    return _getTimeInMillisecond();

                default:
                    return 0;
            }
        }

        private double _getTimeInHour()
        {
            switch (_unit)
            {
                case TimeUnit.Hour:
                    return _time;

                case TimeUnit.Minute:
                    return _time / 60.0;

                case TimeUnit.Second:
                    return _time / 3600.0;

                case TimeUnit.Millisecond:
                    return _time / 3600000.0;

                default:
                    throw new InvalidOperationException();
            }
        }

        private double _getTimeInMinute()
        {
            switch (_unit)
            {
                case TimeUnit.Hour:
                    return _time * 60.0;

                case TimeUnit.Minute:
                    return _time;

                case TimeUnit.Second:
                    return _time / 60.0;

                case TimeUnit.Millisecond:
                    return _time / 60000.0;

                default:
                    throw new InvalidOperationException();
            }
        }

        private double _getTimeInSecond()
        {
            switch (_unit)
            {
                case TimeUnit.Hour:
                    return _time * 3600.0;

                case TimeUnit.Minute:
                    return _time * 60.0;

                case TimeUnit.Second:
                    return _time;

                case TimeUnit.Millisecond:
                    return _time / 1000.0;

                default:
                    throw new InvalidOperationException();
            }
        }

        private double _getTimeInMillisecond()
        {
            switch (_unit)
            {
                case TimeUnit.Hour:
                    return _time * 3600000.0;

                case TimeUnit.Minute:
                    return _time * 60000.0;

                case TimeUnit.Second:
                    return _time * 1000.0;

                case TimeUnit.Millisecond:
                    return _time;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
