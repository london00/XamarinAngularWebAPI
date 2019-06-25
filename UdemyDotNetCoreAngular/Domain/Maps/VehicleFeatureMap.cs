﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UdemyDotNetCoreAngular.Domain.Models;

namespace UdemyDotNetCoreAngular.Domain.Maps
{
    public class VehicleFeaturesMap : IEntityTypeConfiguration<VehicleFeature>
    {
        public void Configure(EntityTypeBuilder<VehicleFeature> builder)
        {
            builder.ToTable("VehicleFeatures");
            builder.HasOne(x => x.Feature).WithMany(x => x.VehicleFeatures).HasForeignKey(x => x.FeatureId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Vehicle).WithMany(x => x.VehicleFeatures).HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(x => new { x.FeatureId, x.VehicleId }).IsUnique(true);
        }
    }
}
