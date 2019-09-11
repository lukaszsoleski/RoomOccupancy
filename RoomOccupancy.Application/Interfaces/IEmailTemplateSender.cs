using RoomOccupancy.Application.Infrastructure.Email;
using RoomOccupancy.Application.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Interfaces
{
    public interface IEmailTemplateSender
    {
        Task<SendEmailResponse> SendGeneralEmailAsync(Message message, string title, string content1, string content2, string buttonText, string buttonUrl );
    }
}
