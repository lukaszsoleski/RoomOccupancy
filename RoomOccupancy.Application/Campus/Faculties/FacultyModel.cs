using AutoMapper;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Faculties
{
    public class FacultyModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Faculty, FacultyModel>().ReverseMap();
        }
    }
}
