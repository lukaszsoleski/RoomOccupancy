using RoomOccupancy.Application.Infrastructure.Email;
using RoomOccupancy.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Notifications
{
    public class EmailSender
    {
        private readonly IEmailTemplateSender _templateSender;

        public EmailSender(IEmailTemplateSender templateSender)
        {
            _templateSender = templateSender;
        }
        public Task<SendEmailResponse> SendEmailVerificationMessage()
        {

            var message = new Message();


            return null; // _templateSender.SendGeneralEmailAsync(messsage);
        }
    }
}
