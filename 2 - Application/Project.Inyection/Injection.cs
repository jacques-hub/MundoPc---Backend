using Microsoft.Extensions.DependencyInjection;
using Project.Domain.Entities;
using Project.Infraestructure;
using Project.Infraestructure.Repository;
using Proyect.Implementation;
using Proyect.Interface;
using System;

namespace Project.Inyection
{
    public class Injection
    {
        public static void ConfigurationServices(IServiceCollection servicios)
        {
            servicios.AddDbContext<DataContext>();

            servicios.AddTransient<IProductRepository, ProductRepository>();
            servicios.AddTransient<IRepository<Product>, Repository<Product>>();

            servicios.AddTransient<IUserRepository, UserRepository>();
            servicios.AddTransient<IRepository<User>, Repository<User>>();

            servicios.AddTransient<IBrandRepository, BrandRepository>();
            servicios.AddTransient<IRepository<Brand>, Repository<Brand>>();

            servicios.AddTransient<ICategoryRepository, CategoryRepository>();
            servicios.AddTransient<IRepository<Category>, Repository<Category>>();

            servicios.AddTransient<IProductRepairRepository, ProductRepairRepository>();
            servicios.AddTransient<IRepository<ProductRepair>, Repository<ProductRepair>>();

            servicios.AddTransient<ITechnicalServiceRepository, TechnicalServiceRepository>();
            servicios.AddTransient<IRepository<TechnicalService>, Repository<TechnicalService>>();

            servicios.AddTransient<IMessengerRepository, MessengerRepository>();
            servicios.AddTransient<IRepository<Messenger>, Repository<Messenger>>();

        }
    }
}
