﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class PointHistoryConfigurations : IEntityTypeConfiguration<PointHistory>
{
    public void Configure(EntityTypeBuilder<PointHistory> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.HasOne(x => x.Player)
            .WithMany(x => x.PlayerPointsList)
            .HasForeignKey(x => x.PlayerId);

        builder.Property(x => x.LogDate)
            .IsRequired();

        builder.HasOne(x => x.Tournament)
            .WithMany(x => x.PointHistories)
            .HasForeignKey(x => x.TournamentId).OnDelete(DeleteBehavior.NoAction);
    }
}