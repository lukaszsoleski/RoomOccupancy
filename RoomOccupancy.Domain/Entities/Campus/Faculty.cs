using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class Faculty : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
