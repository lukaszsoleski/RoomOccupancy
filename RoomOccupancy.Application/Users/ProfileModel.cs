using RoomOccupancy.Domain.Entities.Users;

namespace RoomOccupancy.Application.Interfaces.Users
{
    public class ProfileModel : IMapFrom<AppUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public bool IsVerified { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}