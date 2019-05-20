using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Schedule
{
    public class Lecturer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
