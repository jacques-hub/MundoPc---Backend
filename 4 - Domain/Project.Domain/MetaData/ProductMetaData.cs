namespace Project.Domain.MetaData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Project.Domain.Entities;

    public class ProductMetaData : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Image).IsRequired();

            builder.Property(x => x.Code)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.CostPrice)
                .IsRequired();

            builder.Property(x => x.Stock)
                .IsRequired();
        }
    }
}
