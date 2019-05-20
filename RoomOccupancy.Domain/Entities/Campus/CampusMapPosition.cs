using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class CampusMapPosition : IEntity
    {
        public int Id { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
    }
}
