using MediatR;
using RoomOccupancy.Application.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Users.Queries.GetUserJwt
{
   public class GetAuthenticationQuery : IRequest<string>
    {
        public class Handler : IRequestHandler<GetAuthenticationQuery, string>
        {
            private readonly IAuthenticationService authenticationService;

            public Handler(IAuthenticationService authenticationService)
            {
                this.authenticationService = authenticationService;
            }
            public Task<string> Handle(GetAuthenticationQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
