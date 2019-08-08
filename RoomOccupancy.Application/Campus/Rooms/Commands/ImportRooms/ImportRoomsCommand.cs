using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.ImportRooms
{
    public class ImportRoomsCommand : IRequest
    {
        public Stream File { get; set; }
        public class Handler : IRequestHandler<ImportRoomsCommand, Unit>
        {
            public Task<Unit> Handle(ImportRoomsCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
