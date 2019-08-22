using AutoMapper;
using MediatR;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Common;
using RoomOccupancy.Common.Extentions;
using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Reservations.Queries.ValidateCollitions
{
    public class GetReservationCollitionsQuery : IRequest<IEnumerable<Reservation>>
    {
        public Reservation Reservation { get; set; }
        public Room Room { get; set; }


        public class Handler : IRequestHandler<GetReservationCollitionsQuery, IEnumerable<Reservation>>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;

            public Handler(IMapper mapper, IReservationDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<IEnumerable<Reservation>> Handle(GetReservationCollitionsQuery request, CancellationToken cancellationToken)
            {
                //TODO: extract to the separate query
                //var collitions = await _context.Reservations
                //    .AsNoTracking()
                //    .Where(x => HasTimeCollision(x, room, reservation))
                //    .ToListAsync();
                //if (collitions.Any())
                //{
                //    // check cancellation date
                //    foreach (var collition in collitions)
                //    {

                //    }
                //}
                await Task.FromResult(true);

                return new List<Reservation>();
            }
            private static bool HasTimeCollision(Reservation x, Room room, Reservation newR)
            {
                return x.RoomId == room.Id
                    && !x.IsCancelled
                    && x.ReservationDays.HasAny(newR.ReservationDays)
                    && (        // starts in the middle
                    (newR.Start.IsTimeLaterThanInclusive(x.Start) && newR.Start.IsTimeEarlierThan(x.End))
                        ||     // ends too late
                    (newR.End.IsTimeLaterThan(x.Start) && newR.End.IsTimeEarlierThanInclusive(x.End))
                        ||     // starts before and ends after
                    (newR.Start.IsTimeEarlierThanInclusive(x.Start) && newR.End.IsTimeLaterThanInclusive(x.End))
                    );
            }
        }
    }
}
