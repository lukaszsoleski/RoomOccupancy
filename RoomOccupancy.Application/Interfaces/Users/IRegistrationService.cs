﻿using RoomOccupancy.Application.Users;
using RoomOccupancy.Application.Users.Commands.Create;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Interfaces.Users
{
    public interface IRegistrationService
    {
        Task<string> RegisterAsync(RegistrationModel dto);
    }
}
