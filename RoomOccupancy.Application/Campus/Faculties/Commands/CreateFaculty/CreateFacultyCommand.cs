using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Faculties.Commands.CreateFaculty
{
    public class CreateFacultyCommand : IRequest<int>, IMapTo<Faculty>
    {
        public string Name { get; set; }
        public string Acronym { get; set; }
        public IEnumerable<int> Rooms { get; set; }
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

                _context.Faculties.Add(faculty);

                if (request.Rooms != null && request.Rooms.Any())
                {
                    var rooms = await _context.Rooms.Where(x => request.Rooms.Contains(x.Id)).ToListAsync();

                    rooms.ForEach(x => _context.FacultyRooms.Add(new FacultyRoom() { Faculty = faculty, Room = x }));
                }
                await _context.SaveChangesAsync();

                return faculty.Id;
            }
        }

    }

    public class CreateFacultyCommandValidator : AbstractValidator<CreateFacultyCommand>
    {
        public CreateFacultyCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Acronym).NotEmpty();
        }
    }
}