﻿namespace RoomOccupancy.Domain.Entities.Schedule
{
    public class DegreeProgramme : IEntity
    {
        public int Id { get; set; }
        public string FieldOfStudy { get; set; }
        public string Degree { get; set; }
        public string Form { get; set; }
        public string Specialisation { get; set; }
    }
}