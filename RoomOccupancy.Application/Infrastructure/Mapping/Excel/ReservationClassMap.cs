using ExcelMapper;
using RoomOccupancy.Common.Extentions;
using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RoomOccupancy.Application.Infrastructure.Mapping.Excel
{
    public class ReservationClassMap : ExcelClassMap<Reservation>
    {
       
        public ReservationClassMap()
        {
            var startingWeek = DateTime.Now;
            Map(x => x.Room.Building.Number)
                .WithTrim()
                .WithColumnNameMatching(x => Contains(x, "budynek"));
            Map(x => x.Room.Number)
                .WithTrim()
                .WithColumnNameMatching(x => Contains(x, "sala"));
            Map(x => x.Start)
                .WithColumnNameMatching(x => Contains(x,"godz.r."))
                .WithConverter(value => ParseExact(value));
            Map(x => x.End)
              .WithColumnNameMatching(x => Contains(x,"godz.z."))
              .WithConverter(value => ParseExact(value));
            Map(x => x.Subject)
                .WithTrim()
                .WithColumnNameMatching(x => Contains(x,"nazwa p."));
            Map(x => x.ReservationDays)
                .WithTrim()
                .WithColumnNameMatching(x => Contains(x,"nr.dnia"))
                .WithMapping(new Dictionary<string, DaysOfWeek>()
                {
                    { "1", DaysOfWeek.Monday } ,
                    { "2", DaysOfWeek.Tuesday },
                    { "3", DaysOfWeek.Wednesday },
                    { "4", DaysOfWeek.Thursday },
                    { "5", DaysOfWeek.Friday },
                    { "6", DaysOfWeek.Saturday },
                    { "7", DaysOfWeek.Sunday },
                });
            Map(x => x.CancelationDateTime)
                .WithColumnNameMatching(x => Contains(x,"tyd.zakn."))
                .WithConverter(x =>
                {
                    int.TryParse(x?.Trim(), out var value);
                    if (value > 0)
                        return default;

                    return (DateTime?)startingWeek.AddDays(value * 7);
                });
        }
        private static bool Contains(string src, string value) => src.Contains(value, StringComparison.OrdinalIgnoreCase);
        private static DateTime ParseExact(string x)
        {
            var value = x.Trim();
            if (value.Split(":").First().Length == 1)
            {
                value = value.Insert(0, "0");
            }
            return DateTime.ParseExact(value, "HH:mm", new CultureInfo("pl-PL"));
        }



    }
}