using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Persistence.Configurations
{
    public class BuldingFacultyConfigurations : IEntityTypeConfiguration<BuildingFaculty>
    {
        public void Configure(EntityTypeBuilder<BuildingFaculty> builder)
        {
            builder.HasKey(x => new { x.FacultyId, x.BuildingId });

        }
    }
}
