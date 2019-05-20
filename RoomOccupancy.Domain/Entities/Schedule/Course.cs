using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace RoomOccupancy.Domain.Entities.Schedule
{
    public class Course : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

    }
}
