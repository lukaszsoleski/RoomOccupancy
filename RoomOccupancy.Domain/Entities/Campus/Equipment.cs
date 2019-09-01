using System.Collections;
using System.Collections.Generic;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class Equipment : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoomEquipment> RoomsEquipment { get; set; }
    }
}