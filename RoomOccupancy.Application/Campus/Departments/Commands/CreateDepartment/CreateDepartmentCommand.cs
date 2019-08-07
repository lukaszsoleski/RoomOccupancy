using AutoMapper;
using FluentValidation;
using MediatR;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest, IMapTo<Department>
    {
        public string Name { get; set; }

        public class Handler : IRequestHandler<CreateDepartmentCommand, Unit>
        {
            private readonly IReservationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IReservationDbContext _context, IMapper mapper)
            {
                this._context = _context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
            {
                var department = _mapper.Map<Department>(request);

                _context.Departments.Add(department);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }

    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}