using AutoMapper;
using RoomOccupancy.Application.Interfaces;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Users
{
    public class RegistrationModel : IHaveCustomMapping
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FacultyId { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<RegistrationModel, AppUser>()
                 .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
