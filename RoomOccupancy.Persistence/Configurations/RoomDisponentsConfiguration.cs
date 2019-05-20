﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomOccupancy.Domain.Entities.Campus;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoomOccupancy.Persistence.Configurations
{
    public class RoomDisponentsConfiguration : IEntityTypeConfiguration<RoomDisponent>
    {
        public void Configure(EntityTypeBuilder<RoomDisponent> builder)
        {
            // define composite key for the associative table 
            builder.HasKey(x => new { x.RoomId, x.DisponentId });
        }
    }
}
