using AutoMapper;
using MediatR;
using RoomOccupancy.Application.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Reservations.Queries.GetRoomSchedule
{
    public class GetRoomScheduleQuery : IRequest<RoomScheduleViewModel>
    {
        public int RoomId { get; set; }
        public DateTime Date { get; set; }

        public class Handler : IRequestHandler<GetRoomScheduleQuery,RoomScheduleViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;

            public Handler(IMapper mapper, IReservationDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<RoomScheduleViewModel> Handle(GetRoomScheduleQuery request, CancellationToken cancellationToken)
            {
                var schedule = new RoomScheduleViewModel();
                
                var reservations = _context.Reservations
                    .Where(x => !x.IsCancelled && x.RoomId == request.RoomId);


                return schedule;
            }
        }
    }
}
