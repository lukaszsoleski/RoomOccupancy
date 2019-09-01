using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Persistence.Configurations
{
    public class RoomEquipmentConfiguration : IEntityTypeConfiguration<RoomEquipment>
    {
        public void Configure(EntityTypeBuilder<RoomEquipment> builder)
        {
            builder.HasKey(x => new { x.RoomId, x.EquipmentId });
        }
    }
}
