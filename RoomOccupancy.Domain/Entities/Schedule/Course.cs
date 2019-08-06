namespace RoomOccupancy.Domain.Entities.Schedule
{
    public class Course : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LecturerId { get; set; }
        public virtual Lecturer Lecturer { get; set; }

        public int ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}