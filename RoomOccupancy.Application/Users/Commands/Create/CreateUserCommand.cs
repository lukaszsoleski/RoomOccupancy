using MediatR;
using RoomOccupancy.Application.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Users.Commands.Create
{
    public class CreateUserCommand: IRequest<string>
    {


        public class Handler : IRequestHandler<CreateUserCommand, string>
        {
            public Handler(IRegistrationService registrationService)
            {

            }
            public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                return default;
            }
        }
    }
}
