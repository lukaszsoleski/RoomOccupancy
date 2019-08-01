using AutoMapper;
using AutoMapper.Mappers;
using RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomOccupancy.Application.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        private readonly string[] conventions = new string[] { "UpdateCommand", "CreateCommand"};

        public MappingProfile()
        {
            MapEventToEntity(); 
        }

        private void MapEventToEntity()
        {
            CreateMap<CreateRoomCommand, Room>(); 
            // TODO: add conventions 
            //AddConditionalObjectMapper().Where((s, d) => conventions.
            //    Any(x => x == s.Name.Replace(d.Name,"")));
        }

    }
}
