using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.AutoMock;
using RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xunit;
namespace RoomOccupancy.Application.Tests.Campus.Rooms
{
    public class CreateRoomCommandTests     {

        [Fact]
        public async Task Handler_BuildingNotFound_ThrowsNotFoundExWithCorrectMessage()
        {
            //arrange
            var mocker = new AutoMocker();
            var buildingId = 1; 
            var handler = mocker.CreateInstance<CreateRoomCommand.Handler>();
            // mapper moq
            mocker.GetMock<IMapper>().Setup(x => x.Map<Room>(It.IsAny<CreateRoomCommand>()))
                .Returns(new Room() { BuildingId = buildingId });

            mocker.GetMock<IReservationDbContext>().Setup(x => x.Buildings.FindAsync(It.IsAny<int>()))
                .Returns(Task.FromResult((Building)null)); 

            //act
            Func<Task> func = async () => await handler.Handle(new CreateRoomCommand(), CancellationToken.None);
            //assert 
            await func.Should().ThrowExactlyAsync<NotFoundException>()
                .WithMessage($@"Entity ""{typeof(Building).Name}"" ({buildingId}) was not found."); 
        }

        //[Fact]
        //public async Task Handler_RoomCreated_SendsNotification()
        //{

        //}
        //[Fact]
        //public async Task Handler_ValidRequest_RoomIsCreated()
        //{

        //}
    }
}
