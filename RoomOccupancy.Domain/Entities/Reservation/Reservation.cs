using RoomOccupancy.Domain.Entities.Campus;
using System;

namespace RoomOccupancy.Domain.Entities.Reservation
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsCyclical { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}