using RoomOccupancy.Application.Users.Queries.GetUserAuthentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Interfaces.Users
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(CredentialsDto credentialsDTO);
    }
}
