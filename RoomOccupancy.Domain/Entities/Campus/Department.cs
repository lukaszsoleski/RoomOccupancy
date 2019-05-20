using System;
using System.Collections.Generic;

using System.Text;

namespace RoomOccupancy.Domain.Entities.Campus
{
    /// <summary>
    /// Katedra
    /// </summary>
    public class Department : IEntity
    {
        public Department()
        {
            Faculties = new List<Faculty>(); 
        }
        public int Id { get; set ; }
        public string Name { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
