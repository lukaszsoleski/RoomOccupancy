using RoomOccupancy.Domain.Entities.Schedule;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class Disponent : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        //public override string ToString()
        //{
        //    return $"{nameof(Id)} {Id}, {nameof(Name)} {Name} "; 
        //}
    }
}