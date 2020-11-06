namespace Proyect.Implementation
{
    using Project.Domain.Entities;
    using Project.Infraestructure;
    using Project.Infraestructure.Repository;
    using Proyect.Interface;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BrandRepository : IBrandRepository
    {
        private readonly IRepository<Brand> _brandRepository;
        DataContext _context;

        public BrandRepository(IRepository<Brand> brandRepository, DataContext context)
        {
            _brandRepository = brandRepository;
            _context = context;
        }

        public async Task Create(Brand dto)
        {
            var newBrand = new Brand()
            {
                Description = dto.Description,
                IsDeleted = false
            };
            await _brandRepository.Create(newBrand);
        }

        public async Task Delete(Brand dto)
        {
            await _brandRepository.Delete(dto);
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            var brandAll =
                await _brandRepository.GetAll(null, null, false);

            return brandAll.Select(x => new Brand()
            {
                Id = x.Id,
                Description = x.Description,
                IsDeleted = x.IsDeleted
            }).Where(y => y.IsDeleted != true);
        }

        public async Task<Brand> GetById(long UserId)
        {
            return await _brandRepository.GetById(UserId);
        }

        public async Task Update(Brand dto)
        {
            await _brandRepository.Update(dto);
        }
    }
}
