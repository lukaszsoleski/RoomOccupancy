using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using RoomOccupancy.Application.Infrastructure.Email;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using RoomOccupancy.Application.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Infrastructure.Notifications.Templates
{
    public class EmailTemplateSender : IEmailTemplateSender
    {
        private readonly INotificationService _emailService;
        private readonly ILogger<EmailTemplateSender> logger;

        private string _templateNotFoundMessage = "Cannot find general email template in the project resources.";
        public EmailTemplateSender(INotificationService emailService, ILogger<EmailTemplateSender> logger)
        {
            _emailService = emailService;
            this.logger = logger;
        }
        public async Task<SendEmailResponse> SendGeneralEmailAsync(Message message, string title, string content1, string content2, string buttonText, string buttonUrl)
        {
            var templateText = default(string);
            var currAssembly = Assembly.GetAssembly(typeof(EmailTemplateSender));
            var templateResourceName = currAssembly.GetManifestResourceNames().FirstOrDefault(x => x.Contains("GeneralTemplate.html"));
            if (string.IsNullOrEmpty(templateResourceName))
            {
                logger.LogCritical(_templateNotFoundMessage);
                return new SendEmailResponse() {ErrorMessage = _templateNotFoundMessage};
            }
            //TODO add file provider
            using (var reader = new StreamReader(currAssembly.GetManifestResourceStream(templateResourceName), Encoding.UTF8))
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
