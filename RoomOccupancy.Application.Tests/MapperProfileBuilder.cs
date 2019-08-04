using AutoMapper;
using RoomOccupancy.Application.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Tests
{
    public class MapperProfileBuilder
    {
        public static readonly IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        }).CreateMapper();
    }
}
