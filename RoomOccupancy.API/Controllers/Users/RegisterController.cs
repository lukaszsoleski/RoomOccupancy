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
    public class RegisterController : ControllerBase
    {

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(RegistrationModel registrationDto, [FromServices] IRegistrationService registrationService)
        {
                var userId = await registrationService.RegisterAsync(registrationDto);
                return Created("api/register", new { userId });
        }
    }
}