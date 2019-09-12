using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<ProfileModel> GetUserProfile();
        Task<AppUser> GetUser();
        Task<bool> VerifyUser(string userId, string emailToken);
    }
}
