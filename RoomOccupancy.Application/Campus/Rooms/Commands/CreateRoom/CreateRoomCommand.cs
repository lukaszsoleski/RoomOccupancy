using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Commands.CreateRoom
{
    public class CreateRoomCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Space { get; set; }
        public int? Seats { get; set; }
        public string DesignatedUse { get; set; }
        public string ActualUse { get; set; }
        public string Description { get; set; }
        public int Floor { get; set; }
        public int BuildingId { get; set; }
        public int? FacultyId { get; set; }

   //     public class Handler : IRequestHandler<CreateRoomCommand,Unit>

    }
}
