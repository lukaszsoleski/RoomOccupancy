using AutoMapper;
using FluentValidation;
using MediatR;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Buildings.Commands.CreateBuilding
{
    public class CreateBuildingCommand : IRequest, IMapTo<Building>
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public class Handler : IRequestHandler<CreateBuildingCommand, Unit>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;

            public Handler(IMapper mapper, IReservationDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Unit> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
            {
                var building = _mapper.Map<Building>(request);

                _context.Buildings.Add(building);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }

    public class CreateBuildingValidator : AbstractValidator<CreateBuildingCommand>
    {
        public CreateBuildingValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Number).NotEmpty().GreaterThan(0); 
        }
    }
}
