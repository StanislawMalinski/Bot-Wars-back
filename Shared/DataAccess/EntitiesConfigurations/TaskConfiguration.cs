using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class TaskConfiguration : IEntityTypeConfiguration<_Task>
{
    public void Configure(EntityTypeBuilder<_Task> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Refid)
            .IsRequired();
        
        builder.Property(x => x.Synchronized)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.ScheduledOn)
            .IsRequired();
/*
        builder.HasOne(x => x.ParentTask)
            .WithMany(x => x.ChildrenTask)
            .HasForeignKey(x => x.ParentTaskId)
            .OnDelete(DeleteBehavior.NoAction);*/
    }
}