using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RoomOccupancy.Application.Exceptions
{
    public class ImportFormatException : Exception
    {
        public ImportFormatException()
        {
        }

        public ImportFormatException(string message) : base(message)
        {
        }

        public ImportFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImportFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
