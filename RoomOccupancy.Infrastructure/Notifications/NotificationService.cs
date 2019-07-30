using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Infrastructure.Notifications
{
    public class NotificationService : INotificationService
    {
        public Task Notify(Message message)
        {
            return Task.CompletedTask; 
        }
    }
}
