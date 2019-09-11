using RoomOccupancy.Application.Campus.Rooms.Queries.GetRoomsList;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Rooms.Queries.FindRooms
{
    public class FindRoomsResult
    {
        public List<RoomLookupModel> Rooms { get; set; }

        public string NoResultMessage { get; set; }

    }
}
