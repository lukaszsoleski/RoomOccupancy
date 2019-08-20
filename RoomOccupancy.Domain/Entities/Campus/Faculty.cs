using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class Faculty : IEntity
    {
        public Faculty()
        {
            Rooms = new List<FacultyRoom>(); 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public ICollection<FacultyRoom> Rooms { get; private set; }
    }
}
