using RoomOccupancy.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Infrastructure.SystemClock
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
