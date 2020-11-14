namespace Proyect.Interface
{
    using Proyect.Interface.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductRepairRepository
    {
        Task Create(ProductRepairDto dto);
        Task Update(ProductRepairDto dto);
        Task Delete(ProductRepairDto dto);
        Task<ProductRepairDto> GetById(long UserId);
        Task<ProductRepairDto> GetByCode(string code);
        Task<IEnumerable<ProductRepairDto>> GetAll();
    }
}
