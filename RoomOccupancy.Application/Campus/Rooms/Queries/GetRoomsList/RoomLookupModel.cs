﻿using AutoMapper;
using Newtonsoft.Json;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoomOccupancy.Application.Campus.Rooms.Queries.GetRoomsList
{
    public class RoomLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Label { get; set; }
        /// <summary>
        /// eg. sala ćwiczeniowa, komputerowa
        /// </summary>
        public string ActualUse { get; set; }
        public string FacultyLookup { get; set; }
        public int? Seats { get; set; }
        [JsonIgnore]
        public int BuildingId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Room, RoomLookupModel>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(x => $"{x.Floor}/{x.Number}"));
        }
    }
}
