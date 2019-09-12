using MomentSharp;
using RoomOccupancy.Application.Infrastructure.Email;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Users
{
    public class EmailSender
    {
        private readonly IEmailTemplateSender _templateSender;
        // todo: move this to appsettings.json
        private string fromEmail = "noreply@sggwspaces.pl";
        private string from = "No Reply";

        public EmailSender(IEmailTemplateSender templateSender)
        {
            _templateSender = templateSender;
        }
        public Task<SendEmailResponse> SendEmailVerificationMessage(string displayName, string email, string verificationUrl)
        {
            var message = new Message()
            {

                From = from,
                FromEmail = fromEmail,
                To = displayName,
                ToEmail = email,
                Subject = "Rejestracja konta - SGGW Spaces",
                IsHTML = true,
                Content = string.Empty
            };

           return _templateSender.SendGeneralEmailAsync(message,
                "Potwierdź Email",
                $"Cześć {displayName}",
                "Klikająć w poniższy link dokończysz proces rejestracji. Pozwoli Ci to na dodawanie nowych rezerwacji w naszym serwisie.",
                "Potwierdź adres e-mail.",
                verificationUrl
                );
        }
    }
}
