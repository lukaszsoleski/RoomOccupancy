using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Interfaces.Users;
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
        
        public class Handler : IRequestHandler<GetRoomScheduleQuery, RoomScheduleViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;
            private readonly IUserService _userService;

            public Handler(IMapper mapper, IReservationDbContext context, IUserService userService)
            {
                _mapper = mapper;
                _context = context;
                _userService = userService;
            }

            public async Task<RoomScheduleViewModel> Handle(GetRoomScheduleQuery request, CancellationToken cancellationToken)
            {

                var reservations = await _context.Reservations
                    .AsNoTracking()
                    .Where(x => x.RoomId == request.RoomId && !x.IsCancelled)
                    .ProjectTo<ReservationModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                var schedule = new RoomScheduleViewModel();
                schedule.Reservations = reservations.OrderBy(x => x.ReservationDays.FirstOrDefault())
                    .ThenBy(x => x.Start)
                    .ToList();
                var user = await _userService.GetUser();
                if(user != null)
                {
                    schedule.CurrentUserId = user.Id;
                }
                return schedule;
            }
        }
    }
}