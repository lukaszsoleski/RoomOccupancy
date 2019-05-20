using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Reservation
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsCyclical { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }

    }
}
