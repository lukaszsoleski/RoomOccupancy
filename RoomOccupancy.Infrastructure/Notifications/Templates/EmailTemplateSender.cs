using NPOI.HSSF.UserModel;
using RoomOccupancy.Application.Infrastructure.Email;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Infrastructure.Notifications.Templates
{
    public class EmailTemplateSender : IEmailTemplateSender
    {
        private readonly INotificationService _emailService;

        public EmailTemplateSender(INotificationService emailService)
        {
            _emailService = emailService;
        }
        public async Task<SendEmailResponse> SendGeneralEmailAsync(Message message, string title, string content1, string content2, string buttonText, string buttonUrl)
        {
            var templateText = default(string);
            //TODO add file provider
            using (var reader = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream("RoomOccupancy.Infrastructure.Notifications.Templates.GeneralTemplate.html"), Encoding.Unicode))
            {
                templateText = await reader.ReadToEndAsync();

            }

            templateText = templateText.Replace("--Title--", title)
                   .Replace("--Content1--", content1)
                   .Replace("--Content2--", content2)
                   .Replace("--ButtonText--", buttonText)
                   .Replace("--ButtonUrl--", buttonUrl);
            message.Content = templateText;
            message.IsHTML = true;

            var response = await _emailService.SendAsync(message);
            if (!response.Successful && Debugger.IsAttached)
            {
                Debugger.Break();
            }

            return response;

        }
    }

}
