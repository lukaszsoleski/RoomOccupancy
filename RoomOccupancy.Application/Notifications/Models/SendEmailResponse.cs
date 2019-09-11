using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Infrastructure.Email
{
    public class SendEmailResponse
    {
        public bool Successful => ErrorMessage == null;

        public string ErrorMessage { get; set; }
    }
}
