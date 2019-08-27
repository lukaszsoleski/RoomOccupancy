using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MomentSharp;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Reservations.Queries.GetRoomSchedule;
using RoomOccupancy.Common;
using RoomOccupancy.Domain.Entities.Campus;
using RoomOccupancy.Domain.Entities.Reservation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Reservations.Queries.ValidateCollitions
{
    public class GetReservationCollitionsQuery : IRequest<List<ReservationModel>>
    {
        public Reservation Reservation { get; set; }
        public int RoomId { get; set; }

        public class Handler : IRequestHandler<GetReservationCollitionsQuery, List<ReservationModel>>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;

            public Handler(IMapper mapper, IReservationDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<ReservationModel>> Handle(GetReservationCollitionsQuery request, CancellationToken cancellationToken)
            {
                var collitions = await _context.Reservations
                    .AsNoTracking()
                    .Where(x => HasTimeCollision(x, request.RoomId, request.Reservation))
                    .ProjectTo<ReservationModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return collitions;
            }

            private static bool HasTimeCollision(Reservation x, int roomId, Reservation newR)
            {
                var days = (newR.IsCyclical) ? newR.ReservationDays : ((int)newR.Start.DayOfWeek).GetDay();

                return x.RoomId == roomId
                    && !x.IsCancelled
                    // day is between inclusive
                    && newR.Start.IsBetween(x.Start,x.End, DateTimeParts.Day) || newR.Start.IsSame(x.Start,DateTimeParts.Day) || newR.Start.IsSame(x.End, DateTimeParts.Day)
                    // check week day ( ps. I know it's not the best solution, but it's the first one I came up with ;_; so it may cause problems in marginal situations) 
                    && ( newR.IsCyclical && x.ReservationDays.HasAny(days) || !newR.IsCyclical && x.ReservationDays.HasAny(((int)newR.Start.DayOfWeek).GetDay()))
                    && (
                    (StartsInBetween(x, newR))
                        ||
                    (EndsInBetween(x, newR))
                        ||
                    (OverlapsCurrent(x, newR))
                    );
            }

            #region Helpers

            private static bool OverlapsCurrent(Reservation x, Reservation newR)
            {
                return newR.Start.IsTimeEarlierThanInclusive(x.Start) && newR.End.IsTimeLaterThanInclusive(x.End);
            }

            private static bool EndsInBetween(Reservation x, Reservation newR)
            {
                return newR.End.IsTimeLaterThan(x.Start) && newR.End.IsTimeEarlierThanInclusive(x.End);
            }

            private static bool StartsInBetween(Reservation x, Reservation newR)
            {
                return newR.Start.IsTimeLaterThanInclusive(x.Start) && newR.Start.IsTimeEarlierThan(x.End);
            }

            #endregion Helpers
        }
    }
}