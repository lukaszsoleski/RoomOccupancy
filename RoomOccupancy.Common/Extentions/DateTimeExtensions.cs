using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Common
{
    /// <summary>
    /// Fluent extension for HH:mm time format.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Compares the HH:mm time format and returns an indication of their values.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="hour"></param>
        public static int CompareTime(this DateTime @this, DateTime hour)
        {
            if (@this.Hour.Equals(hour.Hour))
                return @this.Minute.CompareTo(hour.Minute);

            return @this.Hour.CompareTo(hour.Hour);
        }

        public static bool IsTimeEarlierThanInclusive(this DateTime @this, DateTime hour)
        {
            var x = @this.CompareTime(hour);
            return x < 0 || x == 0;
        }

        public static bool IsTimeLaterThanInclusive(this DateTime @this, DateTime hour)
        {
            var x = @this.CompareTime(hour);
            return x > 0 || x == 0;
        }

        public static bool IsTimeEarlierThan(this DateTime @this, DateTime hour)
        {
            return @this.CompareTime(hour) < 0;
        }
        public static bool IsTimeLaterThan(this DateTime @this, DateTime hour)
        {
            return @this.CompareTime(hour) > 0;
        }


    }
}
