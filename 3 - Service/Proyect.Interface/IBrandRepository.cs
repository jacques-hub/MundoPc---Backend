namespace Proyect.Interface
{
    using Project.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBrandRepository
    {
        Task Create(Brand dto);
        Task Update(Brand dto);
        Task Delete(Brand dto);
        Task<Brand> GetById(long UserId);
        Task<IEnumerable<Brand>> GetAll();
    }
}
