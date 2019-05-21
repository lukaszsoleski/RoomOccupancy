using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class RoomDisponent
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int DisponentId { get; set; }
        public Disponent Disponent { get; set; }
    }
}
