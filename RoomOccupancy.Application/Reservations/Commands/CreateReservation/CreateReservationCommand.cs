using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Application.Notifications;
using RoomOccupancy.Application.Reservations.Queries.ValidateCollitions;
using RoomOccupancy.Domain.Entities.Reservation;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest<int?>
    {
        public ReservationModel Reservation { get; set; }
        public string UserId { get; set; }
        public class Handler : IRequestHandler<CreateReservationCommand, int?>
        {
            private readonly IReservationDbContext context;
            private readonly IMapper mapper;
            private readonly IMediator mediator;
            private readonly INotificationService notificationService;
            private readonly IUserService userService;

            private AppUser user;

            public Handler(IReservationDbContext context, IMapper mapper, IMediator mediator, INotificationService notificationService, IUserService userService)
            {
                this.context = context;
                this.mapper = mapper;
                this.mediator = mediator;
                this.notificationService = notificationService;
                this.userService = userService;
            }
            public async Task<int?> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
            {
                // Map dto to the entity type
                var reservation = mapper.Map<Reservation>(request.Reservation);

                // Get current user using http authorization header
                user = await userService.GetUser();
                if (user == null)
                    return default;

                // Check if the reservation does not collide with any other one
                var conflicts = await mediator.Send(new GetReservationConflictsQuery() { Reservation = reservation, RoomId = reservation.RoomId });

                //  Notify user about reservation conflict
                if (conflicts.Any())
                    throw new ReservationConflictException($"{string.Join(", ", conflicts.Select(x => x.ToString()))}", $"{reservation}");
               
                // Verify if the user has the privileges to make a reservation
                if (! await IsUserAuthorized(reservation))
                {
                    // Mark the reservation as unauthorised
                    reservation.AwaitsAcceptance = true;
                    // Send acceptance request
                    await SendReservationRequest(reservation, request.UserId);
                }
                
                // Assign an app user to the reservation
                reservation.AppUser = user;
                
                // Start tracking reservation object
                context.Reservations.Add(reservation);
                
                // Save changes to the database
                await context.SaveChangesAsync();

                // Return new Id
                return reservation.Id;
            }


            private async Task<bool> IsUserAuthorized(Reservation reservation)
            {
                if (user == null || !user.IsVerified)
                    return false;

                var userFaculty = user.FacultyId;

                if (!userFaculty.HasValue)
                    return false;

                var roomFacultyIds = await context.FacultyRooms
                    .Where(x => x.RoomId == reservation.RoomId)
                    .Select(x => x.FacultyId)
                    .ToListAsync();

                return roomFacultyIds.Contains(userFaculty.Value);
            }

            private async Task SendReservationRequest(Reservation reservation, string userId)
            {
            }
        }
    }
}
