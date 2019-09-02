using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Users;

namespace RoomOccupancy.Domain.Entities.Schedule
{
    public class Lecturer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DisponentId { get; set; }
        public virtual Disponent Disponent { get; set; }

        public AppUser AppUser { get; set; }
    }
}