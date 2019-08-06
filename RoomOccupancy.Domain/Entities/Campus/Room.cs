using System.Collections.Generic;

namespace RoomOccupancy.Domain.Entities.Campus
{
    public class Room : IEntity
    {
        public Room()
        {
            Equipment = new List<Equipment>();
            Faculties = new List<Faculty>();
        }

        public int Id { get; set; }

        /// <summary>
        /// A room label that consists of the number of the building, floor and room.
        /// </summary>
        public string Name { get; set; }

        public float? Space { get; set; }

        /// <summary>
        /// Available seats for students.
        /// </summary>
        public int? Seats { get; set; }

        public string DesignatedUse { get; set; }
        public string ActualUse { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }

        /// <summary>
        /// Building in which the room is located.
        /// </summary>
        public virtual Building Building { get; set; }

        public int BuildingId { get; set; }

        public int? DisponentId { get; set; }
        public virtual Disponent Disponent { get; set; }

        /// <summary>
        /// Assigned faculties.
        /// </summary>
        public virtual ICollection<Faculty> Faculties { get; }

        /// <summary>
        /// Assigned equipment.
        /// </summary>
        public virtual ICollection<Equipment> Equipment { get; }
    }
}