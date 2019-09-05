using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Exceptions
{
    public class InvalidCredentialException : Exception
    {
        public InvalidCredentialException() : base("Invalid username or password.")
        {

        }
    }
}
