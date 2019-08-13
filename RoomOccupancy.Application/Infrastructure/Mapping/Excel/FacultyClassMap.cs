using ExcelMapper;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Infrastructure.Mapping
{
    public class FacultyClassMap : ExcelClassMap<Faculty>
    {
        public FacultyClassMap()
        {
            Map(x => x.Name)
               .WithColumnNameMatching(x => x.Trim().Equals("nazwa",StringComparison.OrdinalIgnoreCase));

            Map(x => x.Acronym)
                .WithColumnNameMatching(x => x.Trim().Contains("skrócona", StringComparison.OrdinalIgnoreCase));
        }
    }
}
