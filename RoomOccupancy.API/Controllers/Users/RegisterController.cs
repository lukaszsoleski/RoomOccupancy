using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Post(RegistrationModel registrationDto, [FromServices] IRegistrationService registrationService)
        {
            //Validation of the model is done by using the ApiController tag.
            // Catch exception thrown by registration service. 
            try
            {
                var userId = await registrationService.RegisterAsync(registrationDto);
                return Created("api/register", userId);
            }
            catch (InvalidOperationException e)
            {
                // Return 400 status code with error message
                return BadRequest(e.Message);
            }
        }
    }
}