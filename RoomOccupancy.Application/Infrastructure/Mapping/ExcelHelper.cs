using ExcelMapper;
using RoomOccupancy.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using NPOI;
using NPOI.SS.UserModel;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace RoomOccupancy.Application.Infrastructure.Mapping
{
    public class ExcelHelper
    {
        public static IEnumerable<TEntity> Load<TEntity, TConfiguration>(Settings settings) where TConfiguration : ExcelClassMap<TEntity>, new()
        {
            IEnumerable<TEntity> entities;
            try
            {
                using (var importer = new ExcelImporter(settings.File))
                {
                    var index = GetHeadingIndex(settings.File);

                    if (index < 0)
                        throw new InvalidDataException("The file does not contain datasheet data.");

                    importer.Configuration.RegisterClassMap<TConfiguration>();

                    var sheet = importer.ReadSheet();

                    sheet.HeadingIndex = index; 



                    entities = sheet.ReadRows<TEntity>();
                }
            }
            catch (Exception ex)
            {
                var message = $"Cannot load collection of {typeof(TEntity).Name} type from the give file stream.";
                
                throw new ImportFormatException(message, ex);
            }

            return entities;
        }
        private static int GetHeadingIndex(Stream file)
        {
            var sheet = WorkbookFactory.Create(file).GetSheetAt(0);
            var rowIndex = 0;
            IRow cells;

            while ((cells=sheet.GetRow(rowIndex + 1))!= null)
            {
                if (cells.Any(x => x.CellType != CellType.Blank))
                    return rowIndex;
               
                rowIndex++; 
            }
           
            return -1; 
        }

        public class Settings
        {
            public Stream File { get; set; }
            public int HeadingIndex { get; set; } = 0;
        }
    }
}