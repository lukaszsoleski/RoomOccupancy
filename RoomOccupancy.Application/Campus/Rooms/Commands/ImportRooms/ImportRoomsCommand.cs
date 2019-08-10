using ExcelMapper;
using MediatR;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Domain.Entities.Campus;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.ImportRooms
{
    public class ImportRoomsCommand : IRequest
    {
        public Stream File { get; set; }

        public class Handler : IRequestHandler<ImportRoomsCommand, Unit>
        {
            /// <summary>
            /// Zero based sheet row heading index.
            /// </summary>
            private int headingIndex = 1;

            public async Task<Unit> Handle(ImportRoomsCommand request, CancellationToken cancellationToken)
            {
                var rooms = ExcelHelper.Load<Room,RoomExcelClassMap>(new ExcelHelper.Settings() { File = request.File, HeadingIndex = 1 }); 



                return Unit.Value;
            }

        }
    }
}