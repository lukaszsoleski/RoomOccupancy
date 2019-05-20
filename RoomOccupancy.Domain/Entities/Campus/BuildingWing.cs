using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class BuildingWing : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}