using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Campus.Buildings.Queries;
using RoomOccupancy.Application.Campus.Rooms.Queries;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers.Campus
{
    public class RoomsController : BaseController
    {
        /// <summary>
        /// Get rooms related to the building.
        /// </summary>
        /// <param name="number">Building campus number.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Building(int? number)
        {
            var query = new GetRoomsQuery();

            if (number.HasValue)
            {
                var building = await Mediator.Send(new GetBuildingQuery() { Number = number.Value });
                query.ValuePropertyFilter = x => x.BuildingId == building.Id && x.Seats.HasValue;
            }
            else
            {
                query.ValuePropertyFilter = x => x.Seats.HasValue;
                query.IncludeBuildingNo = true;
            }

            // TODO: Filter rooms based on user role.

            var rooms = await Mediator.Send(query);

            return Ok(rooms);
        }
    }

}