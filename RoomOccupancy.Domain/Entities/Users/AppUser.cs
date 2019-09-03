using Microsoft.AspNetCore.Identity;
using RoomOccupancy.Domain.Entities.Campus;

namespace RoomOccupancy.Domain.Entities.Users
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsVerified { get; set; }
        public int? FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}
