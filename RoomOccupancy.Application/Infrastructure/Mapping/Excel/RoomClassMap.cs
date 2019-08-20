using ExcelMapper;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Security.Cryptography.X509Certificates;

namespace RoomOccupancy.Application.Infrastructure.Mapping
{
    public class RoomClassMap : ExcelClassMap<Room>
    {
        public RoomClassMap()
        {
            Map(x => x.Building.Number)
                .WithColumnNameMatching(x => x.Contains("budynek", StringComparison.OrdinalIgnoreCase));

            Map(x => x.Number)
                .WithColumnNameMatching(x => x.Contains("nr pom.", StringComparison.OrdinalIgnoreCase));
            Map(x => x.Name)
                .WithColumnNameMatching(x => x.Contains("nazwa", StringComparison.OrdinalIgnoreCase));

            Map(x => x.Floor)
                .WithColumnNameMatching(x => x.Contains("kondygnacja", StringComparison.OrdinalIgnoreCase));

            Map(x => x.DesignatedUse)
                .WithColumnNameMatching(x => x.Contains("projekt", StringComparison.OrdinalIgnoreCase));

            Map(x => x.ActualUse)
                .WithColumnNameMatching(x => x.Contains("opis", StringComparison.OrdinalIgnoreCase));

            Map(x => x.Space)
                .WithColumnNameMatching(x => x.Contains("powierzchnia", StringComparison.OrdinalIgnoreCase));

            Map(x => x.Seats)
                .WithColumnNameMatching(x => x.Contains("miejsc", StringComparison.OrdinalIgnoreCase));

            Map(x => x.Disponent.Name)
                .WithColumnNameMatching(x => x.Contains("dysponent", StringComparison.OrdinalIgnoreCase))
                .WithEmptyFallback("pow. Ogólna");
        }
    }
}