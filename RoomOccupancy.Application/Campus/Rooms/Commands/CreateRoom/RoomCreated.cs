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
    //INotification - This is only an indicator interface. It does not force us to implement any special method.
    public class RoomCreated : INotification
    {
        public int RoomId { get; set; }
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
                await _notificationService.Notify(
                    new Message() { Body = $"Room with id {notification.RoomId} has been created"}
                    );

            }
        }

    }
}
