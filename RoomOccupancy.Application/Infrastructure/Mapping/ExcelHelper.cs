using ExcelMapper;
using RoomOccupancy.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

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
                    importer.Configuration.RegisterClassMap<TConfiguration>();

                    var sheet = importer.ReadSheet();

                    sheet.HeadingIndex = settings.HeadingIndex;

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

        public class Settings
        {
            public Stream File { get; set; }
            public int HeadingIndex { get; set; } = 0;
        }
    }
}