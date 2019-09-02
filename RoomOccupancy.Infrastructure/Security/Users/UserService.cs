using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Infrastructure.Users;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Interfaces.Users;
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

        public UserService(IReservationDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _context = context;
            _mapper = mapper;

        }

        public async Task<ProfileModel> GetUserProfile()
        {
            // user id claim
            var userId = _caller.Claims.FirstOrDefault(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Id) ?? throw new InvalidCredentialException();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId.Value) ?? throw new InvalidCredentialException();

            return _mapper.Map<ProfileModel>(user);
        }
    }
}
