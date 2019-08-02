using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Campus.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
    {
        public UpdateRoomCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.BuildingId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ActualUse).NotEmpty();
            RuleFor(x => x.Floor).NotEmpty();
        }
    }
}
