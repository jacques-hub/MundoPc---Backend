namespace Project.Domain.MetaData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Project.Domain.Entities;
    using System;

    public class TechnicalServiceMetaData : IEntityTypeConfiguration<TechnicalService>
    {
        public void Configure(EntityTypeBuilder<TechnicalService> builder)
        {
            builder.Property(x => x.SerialNumber)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Observations)
                .HasMaxLength(9999999)
                .IsRequired();

            builder.Property(x => x.EquipmentFailure)
                .HasMaxLength(9999999)
                .IsRequired();

            builder.Property(x => x.DateReceived)
                .IsRequired();

            builder.Property(e => e.ServiceStatus)
                .HasMaxLength(15)
                .IsRequired()
                .HasConversion(
                  x => x.ToString(),
                  x => (ServiceStatus)Enum.Parse(typeof(ServiceStatus), x)
                );

            builder.Property(x => x.AccessoriesReceived)
                .HasMaxLength(9999999);

            builder.Property(e => e.Diagnostic)
                .HasMaxLength(9999999);
        }
    }
}
