using ExcelMapper;
using RoomOccupancy.Application.Reservations;
using RoomOccupancy.Common.Extentions;
using RoomOccupancy.Domain.Entities.Reservation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace RoomOccupancy.Application.Infrastructure.Mapping.Excel
{
    public class ReservationClassMap : ExcelClassMap<ReservationImportModel>
    {
       
        public ReservationClassMap()
        {
            var startingWeek = DateTime.Now;
            Map(x => x.BuildingNumber)
                .WithTrim()
                .WithColumnNameMatching(x => Contains(x, "budynek"));
            Map(x => x.RoomNumber)
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
            Map(x => x.WeekDays)
                .WithColumnName("Nr.Dnia");
            Map(x => x.WeeksCount)
                .WithTrim()
                .WithColumnNameMatching(x => Contains(x, "tyd.zakn."));
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