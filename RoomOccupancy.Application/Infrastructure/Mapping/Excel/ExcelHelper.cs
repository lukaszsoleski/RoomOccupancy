using ExcelMapper;
using Microsoft.EntityFrameworkCore.Internal;
using NPOI.SS.UserModel;
using RoomOccupancy.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RoomOccupancy.Application.Infrastructure.Mapping
{
    public static class ExcelHelper
    {
        public static IEnumerable<TEntity> Load<TEntity, TConfiguration>(byte[] content) where TConfiguration : ExcelClassMap<TEntity>, new()
        {
            IEnumerable<TEntity> entities;
            try
            {
                using (var ms = new MemoryStream(content))
                using (var importer = new ExcelImporter(ms))
                {
                    var index = GetHeadingIndex(content);

                    if (index < 0)
                        throw new InvalidDataException("The file does not contain data-sheet data.");

                    importer.Configuration.RegisterClassMap<TConfiguration>();

                    var sheet = importer.ReadSheet();

                    //sheet.HasHeading = false;
                    sheet.HeadingIndex = index;

                    entities = sheet.ReadRows<TEntity>().ToList();
                }
            }
            catch (Exception ex)
            {
                var message = $"Cannot load collection of {typeof(TEntity).Name} type from the give file stream.";

                throw new ImportFormatException(message, ex);
            }

            return entities;
        }

        private static int GetHeadingIndex(byte[] file)
        {
            ISheet sheet;
            using (var ms = new MemoryStream(file))
            {
                sheet = WorkbookFactory.Create(ms).GetSheetAt(0);
            }

            // The stream must be copied because it is closed by the next line

            var rowIndex = 0;
            IRow cells;

            while ((cells = sheet.GetRow(rowIndex)) != null)
            {
                if (cells.Any(x => x.CellType != CellType.Blank))
                    return rowIndex;

                rowIndex++;
            }

            return -1;
        }
    }
}