using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Interfaces.Users;

namespace RoomOccupancy.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ApiUser")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
           return Ok(await userService.GetUserProfile());
        }

    }
}