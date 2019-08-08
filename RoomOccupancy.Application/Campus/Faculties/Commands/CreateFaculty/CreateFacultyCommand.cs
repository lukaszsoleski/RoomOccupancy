using AutoMapper;
using FluentValidation;
using MediatR;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Faculties.Commands.CreateFaculty
{
    public class CreateFacultyCommand : IRequest<int>, IMapTo<Faculty>
    {
        public string Name { get; set; }

        public int DepartmentId { get; set; }
        public class Handler : IRequestHandler<CreateFacultyCommand,int>
        {
            private readonly IMapper _mapper;
            private readonly IReservationDbContext _context;

            public Handler(IMapper mapper, IReservationDbContext dbContext)
            {
                _mapper = mapper;
                _context = dbContext;
            }

            public async Task<int> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
            {
                var faculty = _mapper.Map<Faculty>(request);

                faculty.Department = await _context.Departments.FindAsync(request.DepartmentId)
                    ?? throw new NotFoundException(typeof(Department).Name, request.DepartmentId);

                _context.Faculties.Add(faculty);

                await _context.SaveChangesAsync();

                return faculty.Id;
            }
        }

    }

    public class CreateFacultyCommandValidator : AbstractValidator<CreateFacultyCommand>
    {
        public CreateFacultyCommandValidator()
        {
            RuleFor(x => x.DepartmentId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty(); 
        }
    }
}