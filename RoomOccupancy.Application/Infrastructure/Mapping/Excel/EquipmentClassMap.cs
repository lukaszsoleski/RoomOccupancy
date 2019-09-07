using ExcelMapper;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Infrastructure.Mapping.Excel
{
    public class EquipmentClassMap : ExcelClassMap<Equipment>
    {
        public EquipmentClassMap()
        {
            Map(x => x.Name)
                .WithColumnNameMatching(x => x.Contains("nazwa",StringComparison.OrdinalIgnoreCase));
        }
    }
}
