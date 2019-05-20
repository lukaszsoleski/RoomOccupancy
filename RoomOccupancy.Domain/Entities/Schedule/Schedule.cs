using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Schedule
{
    public class Schedule : IEntity
    {
        public int Id { get; set; }
    
        public string Year { get; set; }
        public string Semestrial { get; set; }
            
        public int DegreeProgrammeId { get; set; }
        public virtual DegreeProgramme DegreeProgramme { get; set; }
    }   
}
