using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}