using AutoMapper;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Buildings
{
    public class BuildingModel : IHaveCustomMapping
    {



        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Room, BuildingModel>().ReverseMap();
        }
    }
}
