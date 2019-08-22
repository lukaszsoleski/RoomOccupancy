using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Exceptions
{
    public class ReservationCollitionException: Exception
    {
        public ReservationCollitionException(Reservation conflicting, Reservation current) : base($"The given reservation {current} is in conflict with  {conflicting}") { }
    }
}
