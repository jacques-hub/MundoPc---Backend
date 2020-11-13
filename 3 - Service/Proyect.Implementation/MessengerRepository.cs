namespace Proyect.Implementation
{
    using Project.Domain.Entities;
    using Project.Infraestructure;
    using Project.Infraestructure.Repository;
    using Proyect.Interface;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MessengerRepository: IMessengerRepository
    {
        private readonly IRepository<Messenger> _messengerRepository;
        DataContext _context;

        public MessengerRepository(IRepository<Messenger> messengerRepository, DataContext context)
        {
            _messengerRepository = messengerRepository;
            _context = context;
        }

        public async Task Create(Messenger dto)
        {
            var newMessenger = new Messenger()
            {
                name = dto.name,
                email = dto.email,
                phone = dto.phone,
                query = dto.query,
                isAnswered = false,
                IsDeleted = false
            };
            await _messengerRepository.Create(newMessenger);
        }

        public async Task Delete(Messenger dto)
        {
            await _messengerRepository.Delete(dto);
        }

        public async Task<IEnumerable<Messenger>> GetAll()
        {
            var messengerAll =
                await _messengerRepository.GetAll(null, null, false);

            return messengerAll.Select(x => new Messenger()
            {
                Id = x.Id,
                name = x.name,
                email = x.email,
                query = x.query,
                phone = x.phone,
                isAnswered = x.isAnswered,
                IsDeleted = x.IsDeleted
            }).Where(y => y.IsDeleted != true);
        }

        public async Task<Messenger> GetById(long Id)
        {
            return await _messengerRepository.GetById(Id);
        }

        public async Task Update(Messenger dto)
        {
            await _messengerRepository.Update(dto);
        }
    }
}
