using AutoMapper;
using FluentAssertions;
using RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RoomOccupancy.Application.Tests.Mapping
{
    public class MapperConfigurationTests {
        [Fact]
        public void RoomCreated_BuildingInstanceIsNotNull_PropertiesAreMapped()
        {
            // arrange
            var room = new Room()
            {
                Building = new Building()
                {
                    Number = 10
                },
                Id = 100,
                Name = "3/31a",
                ActualUse = "Lab"
            };
            var mapper = MapperProfileBuilder.Mapper;

            // act 
            var roomCreated = mapper.Map<RoomCreated>(room);
            // assert
            roomCreated.BuildingNumber.Should().Be(10);
            roomCreated.RoomId.Should().Be(100);
            roomCreated.Name.Should().Be("3/31a");
            roomCreated.ActualUse.Should().Be("Lab"); 

        }

    }
}
