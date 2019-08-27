using RoomOccupancy.Common.Extentions;
using RoomOccupancy.Domain.Entities.Campus;
using System;

namespace RoomOccupancy.Domain.Entities.Reservation
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        

        /// <summary>
        /// Name for the reservation label.
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        ///  Days of the week on which the cyclic reservation takes place.
        /// </summary>
        public DaysOfWeek ReservationDays { get; set; }

        /// <summary>
        /// Will automatically book another meeting.
        /// </summary>
        public bool IsCyclical { get; set; }

        /// <summary>
        /// Defines if this reservation is still active.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// The room where the meeting is to be held.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// The room where the meeting is to be held.
        /// </summary>
        public virtual Room Room { get; set; }

        public override string ToString()
        {
            return $"{Subject} at {Start} to {End} on days {ReservationDays}";
        }
    }
}