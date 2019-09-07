using ExcelMapper;
using RoomOccupancy.Application.Campus.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Application.Infrastructure.Mapping.Excel
{
    public class RoomEquipmentClassMap : ExcelClassMap<EquipmentModel>
    {
        public RoomEquipmentClassMap()
        {
            Map(x => x.RoomName)
                .WithTrim()
                .WithColumnNameMatching(x => x.Contains("Pomieszczenie", StringComparison.OrdinalIgnoreCase));
            Map(x => x.EquipmentName)
                .WithTrim()
                .WithColumnNameMatching(x => x.Contains("Wyposażenie", StringComparison.OrdinalIgnoreCase));
            Map(x => x.Amount)
                .WithTrim()
                .WithColumnNameMatching(x => x.Contains("Ilość", StringComparison.OrdinalIgnoreCase));
        }
    }
}
