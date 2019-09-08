using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Reservations;
using RoomOccupancy.Application.Reservations.Commands.CancelReservation;
using RoomOccupancy.Application.Reservations.Commands.CreateReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers.Schedule
{
    [Authorize(Policy = "ApiUser")]
    public class ReservationController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(ReservationModel reservation)
        {
            return Created("api/reservation", await Mediator.Send(new CreateReservationCommand() { Reservation = reservation }));
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Cancel(int id)
        {
            return Ok(await Mediator.Send(new CancelReservationCommand() { Id = id }));
        }
    }
}
