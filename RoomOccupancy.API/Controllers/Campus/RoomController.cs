using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Campus.Buildings.Queries;
using RoomOccupancy.Application.Campus.Equipment.Queries;
using RoomOccupancy.Application.Campus.Rooms.Queries;
using RoomOccupancy.Application.Campus.Rooms.Queries.GetRoom;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers.Campus
{
    public class RoomController : BaseController
    {
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id) => Ok(await Mediator.Send(new GetRoomDetailQuery() { Id = id }));

        /// <summary>
        /// Get rooms related to the building.
        /// </summary>
        /// <param name="number">Building number.</param>
        [HttpGet("[action]")]
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
        // room/{id}/equipment
        [HttpGet("{id:int:min(1)}/[action]")]    
        public async Task<IActionResult> Equipment(int id)
        {
            return Ok(await Mediator.Send(new GetRoomEquipmentQuery() { RoomId = id }));
        }
    }

}