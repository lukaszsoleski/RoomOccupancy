using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Reservations;
using RoomOccupancy.Application.Reservations.Commands.CreateReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers.Schedule
{
    public class ReservationController : BaseController
    {
        [HttpPost]
        [Authorize(Policy = "ApiUser")]
        public async Task<IActionResult> Post(ReservationModel reservation)
        {
            return Created("api/reservation", await Mediator.Send(new CreateReservationCommand() { Reservation = reservation }));
        }
    }
}
