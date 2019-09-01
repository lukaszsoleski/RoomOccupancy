using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class RoomEquipment
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public int Amount { get; set; }
        public bool IsAvailable { get; set; }
    }
}
