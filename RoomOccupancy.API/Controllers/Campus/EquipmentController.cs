using Microsoft.AspNetCore.Mvc;
using RoomOccupancy.Application.Campus.Equipment.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomOccupancy.API.Controllers.Campus
{
    public class EquipmentController: BaseController
    {
        public async  Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetEquipmentQuery()));
        }
    }
}
