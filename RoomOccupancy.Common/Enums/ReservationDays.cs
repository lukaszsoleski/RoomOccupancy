using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RoomOccupancy.Common.Extentions
{
    [Flags]
    public enum DaysOfWeek
    {
        Monday = 1,
        Tuesday = 1 << 2,
        Wednesday = 1 << 3,
        Thursday = 1 << 4, 
        Friday = 1 << 5,
        Saturday = 1 << 6,
        Sunday = 1 << 7,
    }
}
