using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers
{
    [ApiController]
    [Route("")]
    public class WelcomeController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hello()");
        }
    }
}
