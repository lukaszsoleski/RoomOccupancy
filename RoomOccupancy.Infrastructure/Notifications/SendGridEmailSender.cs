using Microsoft.Extensions.Logging;
using RoomOccupancy.Application.Infrastructure.Email;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Infrastructure.Notifications
{
    public class SendGridEmailSender : INotificationService
    {
        private readonly ILogger<SendGridEmailSender> logger;

        public SendGridEmailSender(ILogger<SendGridEmailSender> logger)
        {
            this.logger = logger;
        }
        public string ApiKey => Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        public async Task<SendEmailResponse> SendAsync(Message message)
        {
            var result = new SendEmailResponse();
            string api_key = ApiKey;
            #region validate api key and message
            if (api_key == null)
            {
                result.ErrorMessage = "The environment variable SENDGRID_API_KEY was not found.";
                logger.LogWarning(result.ErrorMessage);
                return result;
            }

            var validator = new MessageValidator();
            var messageValidationResult = validator.Validate(message);
            if (!messageValidationResult.IsValid)
            {
                result.ErrorMessage = "Cannot send email. " + string.Join(" ", messageValidationResult.Errors.Select(x => $"Property {x.PropertyName} failed validation with error: {x.ErrorMessage}."));
                logger.LogError(result.ErrorMessage);
                return result;
            }
            #endregion

            var client = new SendGridClient(api_key);
            var from = new EmailAddress(message.FromEmail, message.From);
            var to = new EmailAddress(message.ToEmail,message.To);
            
            var msg = MailHelper.CreateSingleEmail(from, to,  message.Subject, message.IsHTML ? "" : message.Content, message.IsHTML ? message.Content : null);
            var response = await client.SendEmailAsync(msg);

            if(response.StatusCode != HttpStatusCode.Accepted)
            {
                var body = response.Body.ReadAsStringAsync();
                result.ErrorMessage = $"There were problems with the delivery of the message. Service returned {response.StatusCode} status code. Find more information at https://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/errors.html. --response body: {body}";
                logger.LogError("result.ErrorMessage");
            }
            else
            {
            logger.LogInformation($"Successfully delivered email to {message.To} ({message.ToEmail}) from {message.From} ({message.FromEmail}).");
            }
            return result;
        }
    }
}
