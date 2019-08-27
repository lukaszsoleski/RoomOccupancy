using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Exceptions
{
    public class ReservationConflictException: Exception
    {
        public ReservationConflictException(string conflicting, string current) : base($"The given reservation {current} is in conflict with  {conflicting}") { }
    }
}
