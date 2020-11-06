namespace Proyect.Implementation
{
    using Project.Domain.Entities;
    using Project.Infraestructure;
    using Project.Infraestructure.Repository;
    using Proyect.Interface;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryRepository: ICategoryRepository
    {
        private readonly IRepository<Category> _categoryRepository;
        DataContext _context;

        public CategoryRepository(IRepository<Category> brandRepository, DataContext context)
        {
            _categoryRepository = brandRepository;
            _context = context;
        }

        public async Task Create(Category dto)
        {
            var newCategoty = new Category()
            {
                Description = dto.Description,
                IsDeleted = false
            };
            await _categoryRepository.Create(newCategoty);
        }

        public async Task Delete(Category dto)
        {
            await _categoryRepository.Delete(dto);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var categoryAll =
                await _categoryRepository.GetAll(null, null, false);

            return categoryAll.Select(x => new Category()
            {
                Id = x.Id,
                Description = x.Description,
                IsDeleted = x.IsDeleted
            }).Where(y => y.IsDeleted != true);
        }

        public async Task<Category> GetById(long UserId)
        {
            return await _categoryRepository.GetById(UserId);
        }

        public async Task Update(Category dto)
        {
            await _categoryRepository.Update(dto);
        }
    }
}
