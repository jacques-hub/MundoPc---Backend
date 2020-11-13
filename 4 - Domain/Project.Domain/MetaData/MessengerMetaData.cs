namespace Project.Domain.MetaData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Project.Domain.Entities;

    public class MessengerMetaData: IEntityTypeConfiguration<Messenger>
    {
        public void Configure(EntityTypeBuilder<Messenger> builder)
        {
            builder.Property(x => x.email)
                 .HasMaxLength(100)
                 .IsRequired();
            builder.Property(x => x.name)
                 .HasMaxLength(100)
                 .IsRequired();
            builder.Property(x => x.query)
                 .HasMaxLength(9999999)
                 .IsRequired();
        }
    }
}
