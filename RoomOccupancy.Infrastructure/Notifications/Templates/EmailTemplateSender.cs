using RoomOccupancy.Application.Infrastructure.Email;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Infrastructure.Notifications.Templates
{
    public class EmailTemplateSender : IEmailTemplateSender
    {
        public async Task<SendEmailResponse> SendGeneralEmailAsync(Message message, string title, string content1, string content2, string buttonText, string buttonUrl)
        {
            var templateText = default(string);
            //TODO add file provider
            using (var reader = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream("RoomOccupancy.Infrastructure.Notifications.Templates.GeneralTemplate.html"), Encoding.Unicode))
            {
                templateText = await reader.ReadToEndAsync();
            }

            return null;
        }
    }

}
