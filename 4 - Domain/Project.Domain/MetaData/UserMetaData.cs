namespace Project.Domain.MetaData.LoginMetaData
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Project.Domain.Entities;
    using System;

    public class UserMetaData : IEntityTypeConfiguration<User>
    {       
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);            

            builder.Property(e => e.Telephone)
                .HasMaxLength(10)
                .IsUnicode(false);
            
            builder.Property(e => e.Role)
                .HasMaxLength(15)
                .IsRequired()
                .HasConversion(
                  x => x.ToString(),
                  x => (RoleType)Enum.Parse(typeof(RoleType), x)
                );
        }
    }
}
