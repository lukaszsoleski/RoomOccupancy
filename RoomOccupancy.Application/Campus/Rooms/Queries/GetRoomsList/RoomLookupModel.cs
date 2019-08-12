using AutoMapper;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Rooms.Queries.GetRoomsList
{
    public class RoomLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Room, RoomLookupModel>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(x => x.ToString()));
        }
    }
}
