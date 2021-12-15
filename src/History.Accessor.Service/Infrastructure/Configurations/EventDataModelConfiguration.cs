using History.Accessor.Service.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace History.Accessor.Service.Infrastructure.Configurations;

public class EventDataModelConfiguration : IEntityTypeConfiguration<EventDataModel>
{
    public void Configure(EntityTypeBuilder<EventDataModel> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasIndex(x => x.DateTime);
            
        builder.Property(x => x.DateTime)
            .IsRequired();

        builder.Property(x => x.Message)
            .IsRequired();

        builder.Property(x => x.EventName)
            .IsRequired()
            .HasMaxLength(100);
    }
}