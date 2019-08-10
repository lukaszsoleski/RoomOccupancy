using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class FacultyRoom
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}
