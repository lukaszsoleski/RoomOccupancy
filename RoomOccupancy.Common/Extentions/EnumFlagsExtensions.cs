using System;
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
    }
}
