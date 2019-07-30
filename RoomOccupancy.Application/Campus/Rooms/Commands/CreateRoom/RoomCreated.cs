using MediatR;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom
{
    public class RoomCreated : INotification
    {
        public int RoomId { get; set; }

        public class RoomCreatedHandler : INotificationHandler<RoomCreated>
        {
            private readonly INotificationService _notificationService;

            public RoomCreatedHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task Handle(RoomCreated notification, CancellationToken cancellationToken)
            {
                await _notificationService.Notify(
                    new Message() { Body = $"Room with id {notification.RoomId} has been created"}
                    );

            }
        }

    }
}
