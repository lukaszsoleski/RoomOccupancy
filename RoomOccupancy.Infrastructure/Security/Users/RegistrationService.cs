using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using RoomOccupancy.Application.Infrastructure.Users;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Application.Users;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Infrastructure.Security.Users
{
    public class RegistrationService : IRegistrationService
    {

        private readonly IMapper _mapper;
        private readonly IReservationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RegistrationService(IMapper mapper, UserManager<AppUser> userManager, IReservationDbContext context)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationDTO">Registration data transfer object</param>
        /// <returns>Created user id.</returns>

        public async Task<string> RegisterAsync(RegistrationModel registrationForm)
        {
            // map dto to AppUser class
            var user = _mapper.Map<AppUser>(registrationForm);

            // Try to create the new user
            var result = await _userManager.CreateAsync(user, registrationForm.Password);
            // If it fails throw an exception
            if (!result.Succeeded)
                throw new ValidationException(result.Errors.FirstOrDefault().Description ?? "");
           
            // Commit changes to database
            await _context.SaveChangesAsync();

            return user.Id;
        }

    }
}
