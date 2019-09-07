using AutoMapper;
using ExcelMapper;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;
using RoomOccupancy.Common;
using System.Linq;

namespace RoomOccupancy.Application.Reservations
{
    public class ReservationModel : IHaveCustomMapping
    {
        public ReservationModel()
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
        /// If the user is not authorized to make a reservation, the reservation is awaiting acceptance.
        /// </summary>
        public bool AwaitsAcceptance { get; set; }

        /// <summary>
        /// The room where the meeting is to be held.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// The room where the meeting is to be held.
        /// </summary>
        public string RoomName { get; set; }

        public string UserName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Reservation, ReservationModel>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => $"{src.Room}"))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{(src.AppUser != null ? src.AppUser.FirstName : "")} {(src.AppUser != null ? src.AppUser.LastName : "")}"))
                .ForMember(dest => dest.ReservationDays, opt => opt.MapFrom(src => src.ReservationDays.GetDays()));
                
            configuration.CreateMap<ReservationModel, Reservation>()
                .ForMember(dest => dest.ReservationDays, opt => opt.MapFrom(src => src.ReservationDays.GetDays()));
        }

        public override string ToString()
        {
            var format = "dd/MM/yyyy hh:mm";

            return $"{Subject} at {Start.ToString(format)} to {End.ToString(format)} on days {ReservationDays.Select(x => x + " ")}";
        }
    }
}
