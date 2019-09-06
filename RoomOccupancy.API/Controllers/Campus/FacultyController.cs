using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Campus.Faculties;
using RoomOccupancy.Application.Campus.Faculties.Queries.GetFaculties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers.Campus
{
    public class FacultyController: BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetFacultiesQuery()));
        }
    }
}
