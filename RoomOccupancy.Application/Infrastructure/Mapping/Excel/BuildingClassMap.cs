using ExcelMapper;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Infrastructure.Mapping
{
    public class BuildingClassMap : ExcelClassMap<Building>
    {
        public BuildingClassMap()
        {
            Map(x => x.Number)
                .WithColumnNameMatching(x => x.Contains("numer", StringComparison.OrdinalIgnoreCase));


        }
    }
}
