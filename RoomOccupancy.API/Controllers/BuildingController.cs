using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Atp;
using RoomOccupancy.Application.Campus.Rooms.Queries.GetRoomsList;

namespace RoomOccupancy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IMediator mediator;
        
        public BuildingController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
      
    }
}