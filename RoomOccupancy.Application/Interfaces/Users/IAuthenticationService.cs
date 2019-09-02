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
    public class CredentialsDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
