using ExcelMapper;
using RoomOccupancy.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Infrastructure.Mapping.Excel
{
    public class VerifiedUserClassMap : ExcelClassMap<VerifiedUsersDict>
    {
        public VerifiedUserClassMap()
        {
            Map(x => x.Email)
                .WithTrim()
                .WithColumnNameMatching(x => x.Contains("email", StringComparison.OrdinalIgnoreCase));
        }
    }
}
