using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Equipment.Commands.ImportRoomEquipment
{
    using NPOI.SS.Formula.Functions;
    using RoomOccupancy.Application.Exceptions;
    using RoomOccupancy.Domain.Entities.Campus;
    public class ImportRoomEquipmentCommand : IRequest<Unit>
    {
        public IEnumerable<EquipmentModel> Equipment { get; set; }

        public class Handler : IRequestHandler<ImportRoomEquipmentCommand,Unit>
        {
            private readonly IMapper mapper;
            private readonly IReservationDbContext context;

            public Handler(IMapper mapper, IReservationDbContext context)
            {
                this.mapper = mapper;
                this.context = context;
            }
            public async Task<Unit> Handle(ImportRoomEquipmentCommand request, CancellationToken cancellationToken)
            {
                foreach(var item in request.Equipment)
                {
                    await AddEquipmentItem(item);
                }
                await context.SaveChangesAsync();

                return Unit.Value;

            }
            /// <summary>
            /// Creates new record in RoomEquipment table.  
            /// If the room name is not found, the exception will be thrown.
            /// If the equipment name is not found, it will be added.
            /// </summary>
            /// <param name="item"></param>
            /// <returns></returns>
            private async Task AddEquipmentItem(EquipmentModel item)
            {
                //find room by name
                var room = await context.Rooms
                   .Include(e => e.Building)
                   .FirstOrDefaultAsync(x => $"{x.Floor}.{x.Building.Number}/{x.Number}".Equals(item.RoomName))
                   ?? throw new NotFoundException(typeof(Room), item.RoomName);
                // find equipment by name
                var equipment = await context.Equipment.FirstOrDefaultAsync(x => x.Name == item.EquipmentName);
                // If the equipment does not exist, create a new one.
                if (equipment == null)
                {
                    equipment = mapper.Map<Equipment>(item);

                    context.Equipment.Add(equipment);
                }
                var roomEquipment = new RoomEquipment()
                {
                    Amount = item.Amount,
                    Equipment = equipment,
                    Room = room,
                    IsAvailable = true,
                };
                context.RoomEquipment.Add(roomEquipment);
            }
        }
    }
}
