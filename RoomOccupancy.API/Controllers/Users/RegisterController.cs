using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Application.Users;
using RoomOccupancy.Persistence;

namespace RoomOccupancy.API.Controllers.Users
{
    [Route("api/register")]
    [ApiController]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRegistrationService _registrationService;

        public RegisterController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(RegistrationModel registrationDto)
        {
            var userId = await _registrationService.RegisterAsync(registrationDto);

            return Created("api/register", new { userId });
        }
    }
}
