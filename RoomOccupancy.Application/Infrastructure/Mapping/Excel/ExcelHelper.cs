using ExcelMapper;
using RoomOccupancy.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using NPOI;
using NPOI.SS.UserModel;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using System.Threading.Tasks;

namespace RoomOccupancy.Application.Infrastructure.Mapping
{
    public class ExcelHelper
    {
        public async static Task<IEnumerable<TEntity>> Load<TEntity, TConfiguration>(Settings settings) where TConfiguration : ExcelClassMap<TEntity>, new()
        {
            IEnumerable<TEntity> entities;
            try
            {
                using (var importer = new ExcelImporter(settings.File))
                {
                    var index = await GetHeadingIndex(settings.File);

                    if (index < 0)
                        throw new InvalidDataException("The file does not contain datasheet data.");

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
        private async static Task<int> GetHeadingIndex(Stream file)
        {
            var fileContent = new MemoryStream();
            await file.CopyToAsync(fileContent);
            // The stream must be copied because it is closed by the next line
            var sheet = WorkbookFactory.Create(fileContent).GetSheetAt(0);

            var rowIndex = 0;
            IRow cells;

            while ((cells=sheet.GetRow(rowIndex))!= null)
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
        }
    }
}