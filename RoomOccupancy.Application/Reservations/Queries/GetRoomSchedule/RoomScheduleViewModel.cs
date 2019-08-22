using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Reservations.Queries.GetRoomSchedule
{
    public class RoomScheduleViewModel
    {
        public IEnumerable<RoomScheduleLookupModel> Reservations { get; set; }
    }
}
