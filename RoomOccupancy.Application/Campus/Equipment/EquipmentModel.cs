using AutoMapper;
using RoomOccupancy.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace RoomOccupancy.Application.Campus.Equipment
{
    using RoomOccupancy.Domain.Entities.Campus;

    public class EquipmentModel : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Equipment, EquipmentModel>().ReverseMap(); 
        }
    }
}
