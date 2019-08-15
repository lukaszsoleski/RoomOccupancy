namespace RoomOccupancy.Domain.Entities.Campus
{
    public class Building : IEntity
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)} : {Id}, {nameof(Number)} : {Number}";
        }
    }
}