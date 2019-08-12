using AutoMapper;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Disponents
{
    public class DisponentModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Disponent, DisponentModel>().ReverseMap();
        }
    }
}
