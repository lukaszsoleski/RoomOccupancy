using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
   public class Building : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public int? CampusMapPositionId { get; set; }
        public virtual CampusMapPosition CampusMapPosition { get; set; }
    }
}
