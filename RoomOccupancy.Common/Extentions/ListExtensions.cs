using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomOccupancy.Common.Extentions
{
    public static class ListExtensions
    {
        public static T PopFirst<T>(this List<T> @this)
        {
            if (!@this.Any())
                return default;
            var el = @this.First();
            @this.Remove(el);
            return el;
        }
    }
}
