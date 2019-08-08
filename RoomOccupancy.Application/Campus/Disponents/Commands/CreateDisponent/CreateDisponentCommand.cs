using AutoMapper;
using FluentValidation;
using MediatR;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Campus;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Campus.Disponents.Commands.CreateDisponent
{
    public class CreateDisponentCommand : IRequest<int>, IMapTo<Disponent>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public class Handler : IRequestHandler<CreateDisponentCommand,int>
        {
            private readonly IReservationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IReservationDbContext _context, IMapper mapper)
            {
                this._context = _context;
                _mapper = mapper;
            }

            public async Task<int> Handle(CreateDisponentCommand request, CancellationToken cancellationToken)
            {
                var disponent = _mapper.Map<Disponent>(request);

                _context.Disponents.Add(disponent);

                await _context.SaveChangesAsync();
                return disponent.Id;
            }
        }
    }
    public class CreateDisponentCommandValidator : AbstractValidator<CreateDisponentCommand>
    {
        public CreateDisponentCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Name).NotEmpty(); 

        }
    }
}