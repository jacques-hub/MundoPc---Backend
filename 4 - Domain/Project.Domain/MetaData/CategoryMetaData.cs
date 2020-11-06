namespace Project.Domain.MetaData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Project.Domain.Entities;

    public class CategoryMetaData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Description)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
