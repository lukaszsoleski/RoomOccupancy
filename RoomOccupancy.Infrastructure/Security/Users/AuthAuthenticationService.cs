using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RoomOccupancy.Application.Exceptions;
using RoomOccupancy.Application.Infrastructure.Users;
using RoomOccupancy.Application.Interfaces.Users;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Infrastructure.Security.Users
{
    public class AuthAuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthAuthenticationService(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }
        /// <summary>
        /// Authenticates user and returns jwt token. 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public async Task<string> Authenticate(CredentialsDto credentials)
        {
            //Validate credentials and generate user identity
            ClaimsIdentity identity = null;
            try
            {
                identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            }// rethrow exception
            catch (InvalidOperationException) { throw; }

            return await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

        }

        /// <summary>
        /// Checks user credentials and generates his claims identity. 
        /// </summary>
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName) ?? throw new InvalidCredentialException();
            // check the credentials
            if (!await _userManager.CheckPasswordAsync(userToVerify, password))
                throw new InvalidCredentialException();

            return _jwtFactory.ClaimsIdentity(userName, userToVerify.Id);
        }
    }
}
