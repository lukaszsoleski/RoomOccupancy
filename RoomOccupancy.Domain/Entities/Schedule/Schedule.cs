using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Schedule
{
    public class Schedule : IEntity
    {

        public Schedule()
        {
            Courses = new List<Course>(); 
        }
        public int Id { get; set; }
    
        public string Year { get; set; }
        public string Semestrial { get; set; }

        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }

        public int DegreeProgrammeId { get; set; }
        public virtual DegreeProgramme DegreeProgramme { get; set; }

        public virtual ICollection<Course> Courses { get; private set; }

    }   
}
