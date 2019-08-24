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
        [HttpGet("[action]/{id:int:min(1)}")]
        public async Task<IActionResult> Room(int id)
        {
            return Ok(await Mediator.Send(new GetRoomScheduleQuery() { RoomId = id }));
        }
    }
}
