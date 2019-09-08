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
        public int RoomId { get; set; }
        public int EquipmentId { get; set; }
        public int Amount { get; set; }

        public string RoomName { get; set; }
        public string EquipmentName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<EquipmentModel,Equipment>()
                .ForMember(au => au.Name, map => map.MapFrom(vm => vm.EquipmentName))
                .ReverseMap();
            configuration.CreateMap<RoomEquipment, EquipmentModel>();
                
        }
    }
}
