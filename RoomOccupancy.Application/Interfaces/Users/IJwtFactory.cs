using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Interfaces.Users
{
    public interface IJwtFactory
    {
        Task<string> EncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity ClaimsIdentity(string userName, string id);
    }
}
