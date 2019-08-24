using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Common;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace RoomOccupancy.Application.Reservations.Queries.GetRoomSchedule
{
    public class RoomScheduleQueryValidator : AbstractValidator<GetRoomScheduleQuery>
    {
        public RoomScheduleQueryValidator()
        {
            RuleFor(x => x.RoomId).NotEmpty();
        }
    }
    public class GetRoomScheduleQuery : IRequest<RoomScheduleViewModel>
    {
        public int RoomId { get; set; }
        
        public DateTime? Date { get; set; }

        public class Handler : IRequestHandler<GetRoomScheduleQuery, RoomScheduleViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;
            private readonly IDateTime _dateTime;

            public Handler(IMapper mapper, IReservationDbContext context, IDateTime dateTime)
            {
                _mapper = mapper;
                _context = context;
                _dateTime = dateTime;
            }

            public async Task<RoomScheduleViewModel> Handle(GetRoomScheduleQuery request, CancellationToken cancellationToken)
            {
                if (!request.Date.HasValue)
                    request.Date = _dateTime.Now;

                var reservations = await _context.Reservations
                    .AsNoTracking()
                    // find room and check if reservation is not cancelled
                    .Where(x => x.RoomId == request.RoomId && !x.IsCancelled
                                // check for cyclical or one time event
                                && ((x.IsCyclical && x.CancelationDateTime > request.Date) || (x.End > request.Date)))
                    .ProjectTo<RoomScheduleLookupModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                var schedule = new RoomScheduleViewModel();
                schedule.Reservations = reservations;

                return schedule;
            }
        }
    }
}