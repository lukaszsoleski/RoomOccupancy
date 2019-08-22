using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Reservations.Queries.GetRoomSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers.Schedule
{
    public class ScheduleController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetSchedule(GetRoomScheduleQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
