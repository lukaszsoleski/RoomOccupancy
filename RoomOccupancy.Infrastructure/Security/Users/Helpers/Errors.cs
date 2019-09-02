using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Infrastructure.Users
{
    public static class Errors
    {
        public static string ErrorMessage(this IdentityResult errors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var error in errors.Errors)
            {
                sb.AppendLine(error.Description);
            }
            return sb.ToString();
        }
    }
}
