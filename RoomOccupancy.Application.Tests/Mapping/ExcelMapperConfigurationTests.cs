using FluentAssertions;
using NPOI.SS.Formula.Functions;
using RoomOccupancy.Application.Infrastructure.Mapping;
using RoomOccupancy.Application.Infrastructure.Mapping.Excel;
using RoomOccupancy.Domain.Entities.Campus;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace RoomOccupancy.Application.Tests.Mapping
{
    public class ExcelMapperConfigurationTests
    {
        public ExcelMapperConfigurationTests()
        {
            //fix .net core unsupported encoding issues
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Fact]
        public void RoomsClassMap_MappingReturnsNotEmptyCollection()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Initializer\Files\Zajetosc_Sal_do_POLON_budynek_23.xlsx");
            IEnumerable<Room> rooms;
            var bytes = File.ReadAllBytes(path);

            rooms = ExcelHelper.Load<Room, RoomClassMap>(bytes);

            rooms.Should().NotBeEmpty();
            rooms.ToList().ForEach(x => x.Name.Should().NotBeEmpty());
        }

        [Fact]
        public void FacultyClassMap_MappingReturnsNotEmptyCollection()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Initializer\Files\Wydziały.xlsx");
            IEnumerable<Faculty> faculties;
            var bytes = File.ReadAllBytes(path);
            faculties = ExcelHelper.Load<Faculty, FacultyClassMap>(bytes);

            faculties.Should().NotBeEmpty();
            faculties.ToList().ForEach(x => x.Name.Should().NotBeEmpty());
        }
    }
}