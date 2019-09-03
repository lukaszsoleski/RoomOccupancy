using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.AutoMock;
using RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xunit;
namespace RoomOccupancy.Application.Tests.Campus.Rooms
{
    public class CreateRoomCommandTests     {

        [Fact]
        public async Task Handler_BuildingNotFound_ThrowsNotFoundExWithMessage()
        {
            //arrange
            var mocker = new AutoMocker();
            var buildingId = 1; 
            // mapper moq
            mocker.GetMock<IMapper>().Setup(x => x.Map<Room>(It.IsAny<CreateRoomCommand>()))
                .Returns(new Room() { BuildingId = buildingId });

            mocker.GetMock<IReservationDbContext>().Setup(x => x.Buildings.FindAsync(It.IsAny<int>()))
                .Returns(Task.FromResult((Building)null));

            //act
            var handler = mocker.CreateInstance<CreateRoomCommand.Handler>();

            Func<Task> func = async () => await handler.Handle(new CreateRoomCommand(), CancellationToken.None);
            //assert 
            await func.Should().ThrowExactlyAsync<NotFoundException>()
                .WithMessage($@"Entity {typeof(Building)} ({buildingId}) was not found."); 
        }

        [Fact]
        public async Task Handler_RoomCreated_SendsNotification()
        {
            //arrange
            var hasBeenCalled = false;
            var mocker = new AutoMocker();
            var buildingId = 1;

            mocker.Use(MapperProfileBuilder.Mapper); 
            mocker.GetMock<IMediator>().Setup(x => x.Publish(It.IsNotNull<RoomCreated>(),It.IsAny<CancellationToken>()))
                .Callback( () => hasBeenCalled = true);

            var context = InMemoryContext.Create();
            context.Buildings.Add(new Building() { Id = 1 });
            context.SaveChanges();
            mocker.Use<IReservationDbContext>(context);
            //act
            var handler = mocker.CreateInstance<CreateRoomCommand.Handler>();

            await handler.Handle(new CreateRoomCommand() { BuildingId = buildingId }, CancellationToken.None);

            //assert 
            hasBeenCalled.Should().BeTrue();

            context.Dispose();
        }
        [Fact]
        public async Task Handler_ValidRequest_RoomIsCreated()
        {
            //arrange
            var mocker = new AutoMocker();
            var buildingId = 1;

            mocker.Use(MapperProfileBuilder.Mapper);

            var context = InMemoryContext.Create();
            context.Buildings.Add(new Building() { Id = 1 });
            context.SaveChanges();
            mocker.Use<IReservationDbContext>(context);
            //act
            var handler = mocker.CreateInstance<CreateRoomCommand.Handler>();

            await handler.Handle(new CreateRoomCommand() { BuildingId = buildingId }, CancellationToken.None);

            //assert 
            context.Rooms.Count().Should().Be(1);

            context.Dispose();
        }
    }
}
