namespace Project.Domain.MetaData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Project.Domain.Entities;

    public class ProductRepairMetaData : IEntityTypeConfiguration<ProductRepair>
    {
        public void Configure(EntityTypeBuilder<ProductRepair> builder)
        {

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();
            
            builder.Property(x => x.Code)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
