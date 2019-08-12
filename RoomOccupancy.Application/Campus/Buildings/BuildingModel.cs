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
        public int Id { get; set; }
        public int Number { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Building, BuildingModel>().ReverseMap();
        }
    }
}
