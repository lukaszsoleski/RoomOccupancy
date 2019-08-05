using AutoMapper;
using FluentValidation;
using MediatR;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Buildings.Commands.UpdateBuilding
{
    public class UpdateBuildingCommand : IRequest, IMapTo<Building>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public class Handler : IRequestHandler<UpdateBuildingCommand, Unit>
        {
            private readonly IReservationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IReservationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(UpdateBuildingCommand request, CancellationToken cancellationToken)
            {
                var building = await _context.Buildings.FindAsync();

                _mapper.Map(request, building);

                await _context.SaveChangesAsync(); 

                return Unit.Value;
            }
        }

    }
    public class UpdateBuildingCommandValidator : AbstractValidator<UpdateBuildingCommand>
    {
        public UpdateBuildingCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Number).NotEmpty().GreaterThan(0);
        }
    }
}
