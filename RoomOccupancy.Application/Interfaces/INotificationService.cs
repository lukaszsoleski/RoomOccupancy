using RoomOccupancy.Application.Infrastructure.Email;
using RoomOccupancy.Application.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Interfaces
{
    public interface INotificationService
    {
        Task<SendEmailResponse> SendAsync(Message message);
    }
}
