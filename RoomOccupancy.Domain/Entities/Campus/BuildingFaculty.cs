using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class BuildingFaculty
    {
        public int BuildingId { get; set; }
        public int FacultyId { get; set; }
        public Building Building { get; set; }
        public Faculty Faculty { get; set; }
    }
}
