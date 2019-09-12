using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeFixes;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Application.Reservations.Queries.GetUserReservations;

namespace RoomOccupancy.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ApiUser")]
    public class UserController : BaseController
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
        [HttpGet("[action]")]
        public async Task<IActionResult> Reservations()
        {
          return Ok(await Mediator.Send(new GetUserReservationsQuery()));
        }
        [HttpGet("verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            var userIdDecoded = HttpUtility.UrlDecode(userId).Replace(" ", "+");
            var tokenDecoded = HttpUtility.UrlDecode(token).Replace(" ", "+");
            
            if (!await userService.VerifyUser(userIdDecoded, tokenDecoded))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
