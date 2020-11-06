namespace Project.Domain.MetaData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Project.Domain.Entities;


    public class BrandMetaData: IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
