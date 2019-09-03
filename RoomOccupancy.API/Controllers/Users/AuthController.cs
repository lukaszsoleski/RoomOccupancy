using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Application.Users.Queries.GetUserAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers.Users
{

    [Route("api/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CredentialsDto credentialsDto, [FromServices]IAuthenticationService authenticationService)
            => Ok(await authenticationService.Authenticate(credentialsDto));
    }
}