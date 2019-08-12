using AutoMapper;
using RoomOccupancy.Application.Campus.Buildings;
using RoomOccupancy.Application.Campus.Disponents;
using RoomOccupancy.Application.Campus.Equipment;
using RoomOccupancy.Application.Campus.Faculties;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Rooms
{
    public class RoomModel : IHaveCustomMapping
    {

        public int Id { get; set; }
        public string Number { get; set; }
        public float? Space { get; set; }

        public string ActualUse { get; set; }
        public int Floor { get; set; }

        public BuildingModel Building { get; set; }
        public DisponentModel Disponent { get; set; }

        public ICollection<EquipmentModel> Equipment { get; set; }
        public ICollection<FacultyModel> Faculties { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Room, RoomModel>().ReverseMap();

        }
    }
}
