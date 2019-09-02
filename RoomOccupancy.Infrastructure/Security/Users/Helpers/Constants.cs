using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Infrastructure.Users
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }
    }
}
