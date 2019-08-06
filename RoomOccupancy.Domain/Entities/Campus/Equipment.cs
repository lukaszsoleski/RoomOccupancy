namespace RoomOccupancy.Domain.Entities.Campus
{
    public class Equipment : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}