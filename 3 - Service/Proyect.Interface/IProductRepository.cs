namespace Proyect.Interface
{
    using Proyect.Interface.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductRepository
    {
        Task Create(ProductDto dto);
        Task Update(ProductDto dto);
        Task Delete(long id);
        Task<ProductDto> GetById(long UserId);
        Task<IEnumerable<ProductDto>> GetAll();
        Task<IEnumerable<ProductDto>> GetByCode(string code);
    }
}
