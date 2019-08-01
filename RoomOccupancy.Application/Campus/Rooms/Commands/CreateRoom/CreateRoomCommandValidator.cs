using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
           // RuleFor(x => x.BuildingId).NotEmpty();
           // RuleFor(x => x.Name).NotEmpty();
           // RuleFor(x => x.ActualUse).NotEmpty();
        }
    }
}
