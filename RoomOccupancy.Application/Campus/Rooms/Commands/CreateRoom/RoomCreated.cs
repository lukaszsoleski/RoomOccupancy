using AutoMapper;
using MediatR;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom
{
    //INotification - This is only an indicator interface. It does not force us to implement any special method.
    public class RoomCreated : INotification, IHaveCustomMapping
    {
        public int RoomId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string ActualUse { get; set; }
        public int BuildingNumber { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Room, RoomCreated>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Id));

        }

        // Next, we move on to the implementation of the class that will support the above written Event.
        // Class must implement INotificationHandler, where as a T parameter we have to specify which event we want to support in this class.
        public class RoomCreatedHandler : INotificationHandler<RoomCreated>
        {
            private readonly INotificationService _notificationService;

            public RoomCreatedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task Handle(RoomCreated notification, CancellationToken cancellationToken)
            {
                await _notificationService.SendAsync(
                new Message() { Content = $"Room with id {notification.RoomId} has been created" }
                    );

            }
        }

    }
}
