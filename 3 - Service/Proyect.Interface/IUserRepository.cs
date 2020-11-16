namespace Proyect.Interface
{
    using Project.Domain.Entities;
    using Proyect.Interface.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task Create(UserDto dto);
        Task Update(UserDto dto);
        Task Delete(UserDto dto);
        Task<UserDto> GetById(long UserId);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetByEmail(string email);
        Task<UserDto> Authenticate(string email, string password);
        Task<bool> IsValidUser(string email, string password);
    }
}
