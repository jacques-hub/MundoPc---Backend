namespace Project.Infraestructure
{
    using Microsoft.EntityFrameworkCore;

    using Project.Domain.Entities;
    using Project.Domain.MetaData;
    using Project.Domain.MetaData.LoginMetaData;
    using Project.Infraestructure.Connection;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;

    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StringConnection.GetConnectionStringSql);
            base.OnConfiguring(optionsBuilder);
            //notraking
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entidad in ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Deleted
                            && x.OriginalValues.Properties
                                .Any(p => p.Name.Contains("IsDeleted"))))
            {
                entidad.State = EntityState.Unchanged;
                entidad.CurrentValues["IsDeleted"] = true;
            }

            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            var cascadeFks = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFks)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //=======FOREIGN==KEYS=======================================//

            modelBuilder.Entity<Product>().HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .HasConstraintName("FK_Product_Category");

            modelBuilder.Entity<Product>().HasOne(x => x.Brand)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BrandId)
                .HasConstraintName("FK_Product_Brand");

            modelBuilder.Entity<ProductRepair>().HasOne(x => x.Category)
              .WithMany(x => x.ProductRepairs)
              .HasForeignKey(x => x.CategoryId)
              .HasConstraintName("FK_ProductReparis_Category");

            modelBuilder.Entity<ProductRepair>().HasOne(x => x.Brand)
             .WithMany(x => x.ProductRepairs)
             .HasForeignKey(x => x.BrandId)
             .HasConstraintName("FK_ProductReparis_Brand");


            modelBuilder.Entity<TechnicalService>().HasOne(x => x.User)
              .WithMany(x => x.TechnicalServices)
              .HasForeignKey(x => x.UserId)
              .HasConstraintName("FK_TechnicalService_User");

            modelBuilder.Entity<TechnicalService>().HasOne(x => x.ProductRepair)
             .WithMany(x => x.TechnicalServices)
             .HasForeignKey(x => x.ProductRepairId)
             .HasConstraintName("FK_TechnicalService_ProductRepair");


            //=============================================================//

            modelBuilder.ApplyConfiguration<Product>(new ProductMetaData());       
            modelBuilder.ApplyConfiguration<User>(new UserMetaData());
            modelBuilder.ApplyConfiguration<Brand>(new BrandMetaData());
            modelBuilder.ApplyConfiguration<Category>(new CategoryMetaData());
            modelBuilder.ApplyConfiguration<ProductRepair>(new ProductRepairMetaData());
            modelBuilder.ApplyConfiguration<TechnicalService>(new TechnicalServiceMetaData());
            modelBuilder.ApplyConfiguration<Messenger>(new MessengerMetaData());

            //=============================================================//

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductRepair> ProductRepairs { get; set; }
        public DbSet<TechnicalService> TechnicalServices { get; set; }
        public DbSet<Messenger> Messengers { get; set; }


    }
}
