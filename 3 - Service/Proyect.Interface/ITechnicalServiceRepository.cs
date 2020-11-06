namespace Proyect.Interface
{
    using Proyect.Interface.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITechnicalServiceRepository
    {
        Task Create(TechnicalServiceDto dto);
        Task Update(TechnicalServiceDto dto);
        Task Delete(TechnicalServiceDto dto);
        Task<TechnicalServiceDto> GetById(long UserId);
        Task<TechnicalServiceDto> GetByCode(string code);
        Task<IEnumerable<TechnicalServiceDto>> GetAll();

    }
}
