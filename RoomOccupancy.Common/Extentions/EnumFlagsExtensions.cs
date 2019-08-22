using RoomOccupancy.Common.Extentions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomOccupancy.Common
{
    public static class EnumFlagsExtensions
    {
        /// <summary>
        /// Returns the common elements of the enumeration.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Enum> Intersect(this Enum @this, Enum with)
        {
            foreach (Enum flag in Enum.GetValues(@this.GetType()))
            {
                if (@this.HasFlag(flag) && with.HasFlag(flag))
                {
                    yield return flag;
                }
            }
        }
        /// <summary>
        /// Check if the given enumeration contains any of the flags.
        /// </summary>
        public static bool HasAny(this Enum @this, Enum @enum) => @this.Intersect(@enum).Any();
        public static DaysOfWeek GetDay (this int @this)
        {
            switch (@this)
            {
                case 1: return DaysOfWeek.Monday;
                case 2: return DaysOfWeek.Tuesday;
                case 3: return DaysOfWeek.Wednesday;
                case 4: return DaysOfWeek.Thursday;
                case 5: return DaysOfWeek.Friday;
                case 6: return DaysOfWeek.Saturday;
                case 7: return DaysOfWeek.Sunday;
                default: return DaysOfWeek.None;
            }
        }
        public static DaysOfWeek GetDays(this IEnumerable<int> @this)
        {
            var dayNumbers = @this.Where(x => x > 0 && x <= 7).Distinct().ToList();

            if (!dayNumbers.Any())
                return DaysOfWeek.None;

            var days = dayNumbers.PopFirst().GetDay();

            dayNumbers.ForEach(x => days = days | x.GetDay());

            return days;

        }
    }
}
