using AutoMapper;
using ExcelMapper;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;
using RoomOccupancy.Common;
namespace RoomOccupancy.Application.Reservations.Queries.GetRoomSchedule
{
    public class RoomScheduleLookupModel : IHaveCustomMapping
    {
        public RoomScheduleLookupModel()
        {
            ReservationDays = new List<int>();
        }
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
        public List<int> ReservationDays { get; set; }

        /// <summary>
        /// Will automatically book another meeting.
        /// </summary>
        public bool IsCyclical { get; set; }

        /// <summary>
        /// Date of cancellation of the cyclic reservation.
        /// </summary>
        public DateTime? CancelationDateTime { get; set; }

        /// <summary>
        /// The room where the meeting is to be held.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// The room where the meeting is to be held.
        /// </summary>
        public string RoomName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Reservation, RoomScheduleLookupModel>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => $"{src.Room}"))
                .ForMember(dest => dest.ReservationDays, opt => opt.MapFrom(src => src.ReservationDays.GetDays()));
        }

        public override string ToString()
        {
            return $"{Subject} at {Start} to {End} on days {ReservationDays}";
        }
    }
}
