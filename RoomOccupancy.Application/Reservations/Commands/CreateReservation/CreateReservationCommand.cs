using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Application.Notifications;
using RoomOccupancy.Application.Reservations.Queries.ValidateCollitions;
using RoomOccupancy.Domain.Entities.Reservation;
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
        public class Handler : IRequestHandler<CreateReservationCommand, int?>
        {
            private readonly IReservationDbContext context;
            private readonly IMapper mapper;
            private readonly IMediator mediator;
            private readonly INotificationService notificationService;

            public Handler(IReservationDbContext context, IMapper mapper, IMediator mediator, INotificationService notificationService)
            {
                this.context = context;
                this.mapper = mapper;
                this.mediator = mediator;
                this.notificationService = notificationService;
            }
            public async Task<int?> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
            {
                var reservation = mapper.Map<Reservation>(request.Reservation);

                var collitions = await mediator.Send(new GetReservationCollitionsQuery() { Reservation = reservation, RoomId = reservation.RoomId });

                if (collitions.Any())
                    throw new ReservationConflictException($"{string.Join(", ", collitions.Select(x => x.ToString()))}", $"{reservation}");

                if (!IsUseAuthorized(reservation))
                {
                    SendReservationRequest(reservation);
                    await notificationService.Notify(new Message() { /*TODO*/ });
                    return default;
                }

                context.Reservations.Add(reservation);

                await context.SaveChangesAsync();

                return reservation.Id;
            }

            private void SendReservationRequest(Reservation reservation)
            {
            }

            private bool IsUseAuthorized(Reservation reservation)
            {
                return true;
            }
        }
    }
}
