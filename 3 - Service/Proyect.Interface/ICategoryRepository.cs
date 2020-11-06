namespace Proyect.Interface
{
    using Project.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryRepository
    {
        Task Create(Category dto);
        Task Update(Category dto);
        Task Delete(Category dto);
        Task<Category> GetById(long UserId);
        Task<IEnumerable<Category>> GetAll();
    }
}
