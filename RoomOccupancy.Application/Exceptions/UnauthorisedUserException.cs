using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Exceptions
{
    public class UnauthorisedUserException: Exception
    {
        public UnauthorisedUserException() : base("You're not authorized to perform this operation.")
        {

        }
    }
}
