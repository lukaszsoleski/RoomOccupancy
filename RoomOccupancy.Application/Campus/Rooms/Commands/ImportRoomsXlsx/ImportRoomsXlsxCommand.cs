using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.ImportRoomsXlsx
{
    public class ImportRoomsXlsxCommand : IRequest
    {
        //public IScheet MyProperty { get; set; }
        public class Handler : IRequestHandler<ImportRoomsXlsxCommand>
        {
            public Task<Unit> Handle(ImportRoomsXlsxCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
