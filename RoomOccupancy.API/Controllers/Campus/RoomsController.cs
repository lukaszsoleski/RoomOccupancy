using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Campus.Buildings.Queries;
using RoomOccupancy.Application.Campus.Rooms.Queries;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers.Campus
{
    public class RoomsController : BaseController
    {
        private const string numberErrorMessage = "Building number must be greater then zero.";

        /// <summary>
        /// Get rooms related to this building.
        /// </summary>
        /// <param name="buildingNo">Building campus number.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FromBuilding(int buildingNo)
        {
            var building = await Mediator.Send(new GetBuildingQuery() { Number = buildingNo });
            // TODO: Filter rooms based on user role.
            
            var rooms = await Mediator.Send(new GetRoomsQuery()
            {
                ValuePropertyFilter = x => x.BuildingId == building.Id && x.Seats.HasValue
            });


            return Ok(rooms);
        }
    }
}