using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class Room : IEntity
    {
        public Room()
        {
            Disponents = new List<RoomDisponent>();
            Equipment = new List<Equipment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Space { get; set; }
        public int? Seats { get; set; }
        public string DesignatedUse { get; set; }
        public string ActualUse { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }
        public int BuildingId { get; set; }
        public virtual Building Building { get; set; }
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<RoomDisponent> Disponents { get; private set; }

        public virtual ICollection<Equipment> Equipment { get; private set; }
    }
}
