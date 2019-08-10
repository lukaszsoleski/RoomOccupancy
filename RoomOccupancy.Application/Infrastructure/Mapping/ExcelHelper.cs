using ExcelMapper;
using System.Collections.Generic;
using System.IO;

namespace RoomOccupancy.Application.Infrastructure.Mapping
{
    public class ExcelHelper
    {
        public static IEnumerable<TEntity> Load<TEntity, TConfiguration>(Settings settings) where TConfiguration : ExcelClassMap<TEntity>, new()
        {
            IEnumerable<TEntity> entities;

            using (var importer = new ExcelImporter(settings.File))
            {
                importer.Configuration.RegisterClassMap<TConfiguration>();

                var sheet = importer.ReadSheet();

                sheet.HeadingIndex = settings.HeadingIndex;

                entities = sheet.ReadRows<TEntity>();
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