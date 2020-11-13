namespace Proyect.Interface
{
    using Project.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessengerRepository
    {
        Task Create(Messenger dto);
        Task Update(Messenger dto);
        Task Delete(Messenger dto);
        Task<Messenger> GetById(long UserId);
        Task<IEnumerable<Messenger>> GetAll();
    }
}
