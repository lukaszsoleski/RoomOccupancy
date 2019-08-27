using RoomOccupancy.Application.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Interfaces
{
    public interface INotificationService
    {
        Task Notify(Message message);
        Task Ask(Message message);
    }
}
