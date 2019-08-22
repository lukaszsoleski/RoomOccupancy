using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Reservations.Queries.GetRoomSchedule
{
    public class GetRoomScheduleQuery : IRequest
    {
        public int RoomId { get; set; }
        public DateTime Date { get; set; }


    }
}
