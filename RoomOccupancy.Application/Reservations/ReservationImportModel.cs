using AutoMapper;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Common;
using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoomOccupancy.Application.Reservations
{
    public class ReservationImportModel
    {
        public ReservationImportModel()
        {
            WeekDays = new List<int>();
        }

        public List<int> WeekDays { get; set; }
        public string Subject { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string RoomNumber { get; set; }
        public string BuildingNumber { get; set; }
        public int WeeksCount { get; set; }

        public Reservation Map()
        {
            var reservation = new Reservation();
            reservation.Start = Start;
            reservation.End = End;
            reservation.Subject = Subject;
            reservation.ReservationDays = WeekDays.GetDays();
            reservation.CancelationDateTime = DateTime.Now.AddDays(WeeksCount * 7);
            reservation.Room = new Room();
            reservation.Room.Number = RoomNumber?.Split('/').Last().Trim();
            if (RoomNumber.Contains('/'))
            {
               var floorStr = RoomNumber.Split('/').First().Trim();
               
               int.TryParse(floorStr, out var floor);

               reservation.Room.Floor = floor;
            }

            int.TryParse(BuildingNumber, out var bn);

            reservation.Room.Building = new Building() { Number = bn };
            
            return reservation;
        }
    }
}