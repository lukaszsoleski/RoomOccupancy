using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Notifications
{
    public class Message
    {
        public string From { get; set; }
        public string FromEmail { get; set; }
        public string To { get; set; }
        public string ToEmail { get; set;  }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsHTML { get; set; }
    }

    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.From).NotEmpty();
            RuleFor(x => x.FromEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.To).NotEmpty();
            RuleFor(x => x.ToEmail).NotEmpty().EmailAddress();
            RuleFor(x => x.Subject).NotEmpty().MaximumLength(60);
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
