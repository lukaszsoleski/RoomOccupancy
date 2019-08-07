//using Ganss.Excel;
//using MediatR;
//using RoomOccupancy.Application.Interfaces;
//using RoomOccupancy.Domain.Entities.Campus;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace RoomOccupancy.Application.Campus.Rooms.Commands.ImportRoomsXlsx
//{
//    public class ImportRoomsXlsCommand : IRequest, IHaveExcelMapping
//    {
//        public Stream RoomsFile { get; set; }

//        public void SetExcelMapping(ExcelMapper eM)
//        {
//            eM.AddMapping<Room>("Nr pom.", x => x.Name);
//            eM.AddMapping<Room>("Budynek", x => x.BuildingId);
//            eM.AddMapping<Room>("Kondygnacja, na której znajduje się pomieszczenie", x => x.Floor);
//            eM.AddMapping<Room>("Powierzchnia", x => x.Space);
//            eM.AddMapping<Room>("Opis*** (dla sposób wykorzystania INNE)", x => x.ActualUse);
//            eM.AddMapping<Room>("Liczba miejsc (studentów)**", x => x.Seats);
//           // eM.AddMapping<Room>("Liczba miejsc (studentów)**", x => x.Disponents();
//        }

//        public class Handler : IRequestHandler<ImportRoomsXlsCommand>
//        {
//            public Task<Unit> Handle(ImportRoomsXlsCommand request, CancellationToken cancellationToken)
//            {



//                return Unit.Task;
                
//            }
//           private IEnumerable
//        }
//    }
//}
