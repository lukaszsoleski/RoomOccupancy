using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Infrastructure.Users;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Infrastructure.Security.Users
{
    public class UserService : IUserService
    {
        private readonly ClaimsPrincipal _caller;
        private readonly IReservationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IReservationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<AppUser> userManager)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AppUser> GetUser()
        {
            var userId = _caller.Claims.FirstOrDefault(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Id);

            if (userId == null)
            {
                return null;
            }

            var user = await _context.Users.Include(x => x.Faculty)
                .FirstOrDefaultAsync(x => x.Id == userId.Value);

            return user;
        }
        public async Task<bool> VerifyUser(string userId, string emailToken)
        {
            var user = await _context.Users.FindAsync(userId) ?? throw new NotFoundException(typeof(AppUser), userId);

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);

            if (!result.Succeeded)
            {
                return false;
            }
            var hasCollegeEmail = (await _context.VerifiedUsers.FirstOrDefaultAsync(x => x.Email == user.Email)) != null;

            if(hasCollegeEmail)
            {
                user.IsVerified = true;
            }

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<ProfileModel> GetUserProfile()
        {
            var user = await GetUser() ?? throw new NotFoundException(typeof(AppUser), "");

            return _mapper.Map<ProfileModel>(user);
        }
    }
}
