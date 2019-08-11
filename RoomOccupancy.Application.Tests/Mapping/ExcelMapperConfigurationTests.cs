using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using FluentAssertions;
using System.IO;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Domain.Entities.Campus;
using System.Collections;
using System.Linq;
using NPOI.SS.Formula.Functions;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Tests.Mapping
{
    public class ExcelMapperConfigurationTests
    {
        [Fact]
        public async Task RoomsClassMap_MappingReturnsNotEmptyObjects()
        {
            //fix .net core unsuported encoding issues
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Initializer\Files\Zajetosc_Sal_do_POLON_budynek_23.xlsx");
            IEnumerable<Room> rooms;

            using (var stream = new MemoryStream(File.ReadAllBytes(path), true))
            {
                rooms = await ExcelHelper.Load<Room, RoomClassMap>(new ExcelHelper.Settings() { File = stream });
            }

            rooms.Should().NotBeEmpty();
            rooms.ToList().ForEach(x => x.Name.Should().NotBeEmpty());

        }
        
    }
}
