using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationValidator()
        {
            RuleFor(x => x.Reservation).NotNull();
            RuleFor(x => x.Reservation.Start).NotNull();
            RuleFor(x => x.Reservation.End).NotNull();
            RuleFor(x => x.Reservation.RoomId).GreaterThan(0);
            RuleFor(x => x.Reservation.IsCyclical).NotNull();
        }
    }
}
