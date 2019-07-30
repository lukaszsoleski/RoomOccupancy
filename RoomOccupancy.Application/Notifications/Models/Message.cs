using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Notifications
{
    /// <summary>
    /// Temporary/example implementation of the message type. 
    /// TODO: Create email notification service. 
    /// </summary>
    public class Message
    {

        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
