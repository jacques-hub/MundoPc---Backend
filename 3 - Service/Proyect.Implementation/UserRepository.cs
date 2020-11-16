namespace Proyect.Implementation
{
    using Project.Domain.Entities;
    using Project.Infraestructure;
    using Project.Infraestructure.Repository;
    using Proyect.Interface;
    using Proyect.Interface.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _userRepository;
        DataContext _context;

        public UserRepository(IRepository<User> userRepository, DataContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task Create(UserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                Password = dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName,                
                Role = dto.Role,
                Telephone = dto.Telephone, 
                IsDeleted = false,
                IsActive = true
            };
            await _userRepository.Create(newUser);
        }

        public async Task Delete(UserDto entity)
        {
            var e = await _userRepository.GetById(entity.Id);
            await _userRepository.Delete(e);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var userAll =
                await _userRepository.GetAll(null, null, false);

            return userAll.Select(x => new UserDto()
            {
                Id = x.Id,
                Email = x.Email,
                Password = null,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Role = x.Role,
                Telephone = x.Telephone,
                IsDeleted = x.IsDeleted,
                IsActive = x.IsActive

            }).Where(y => y.IsDeleted != true);
        }

        //no se usa
        public async Task<UserDto> GetById(long UserId)
        {
            var x = await _userRepository.GetById(UserId);
            if (x == null)
                throw new Exception("Ningún Usuario tiene el id que busca");
            return new UserDto()
            {
                Id = x.Id,
                Email = x.Email,
                Password = null,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Role = x.Role,
                Telephone = x.Telephone,
                IsDeleted = x.IsDeleted,
                IsActive = x.IsActive
            };
        }

        public async Task<UserDto> Authenticate(string email, string password)
        {
            User u = await Task.Run(() =>                 
                _userRepository.GetAll().Result.FirstOrDefault(x => 
                    x.Email == email && 
                    x.Password == password &&
                    x.IsDeleted != true)
            );

            if (u == null)
                return null;

            u.Password = null;

            return new UserDto()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Password = u.Password,
                Role = u.Role,
                Telephone = u.Telephone,
                IsActive = u.IsActive,
                IsDeleted = u.IsDeleted
            };
        }


        public async Task Update(UserDto dto)
        {
            var _u = await _userRepository.GetById(dto.Id);
            if (_u == null)
                throw new Exception("Ningún Usuario se corresponde con el Id Ingresado");
            _u.FirstName = dto.FirstName;
            _u.LastName = dto.LastName;
            _u.Password = dto.Password;
            _u.Role = dto.Role;
            _u.Telephone = dto.Telephone;
            await _userRepository.Update(_u);
        }

        //tampoco se usa
        public async Task<bool> IsValidUser(string email, string password)
        {
            User u = await Task.Run(() =>
               _userRepository.GetAll().Result.FirstOrDefault(x =>
                   x.Email == email &&
                   x.Password == password &&
                   x.IsDeleted != true)
            );

            if (u == null)
                return false;

            return true;
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            User u = await Task.Run(() =>
                 _userRepository.GetAll().Result.FirstOrDefault(x =>
                     x.Email == email &&
                     x.IsDeleted != true)
             );

            if (u == null)
                return null;
            u.Password = null;

            return new UserDto()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Password = u.Password,
                Role = u.Role,
                Telephone = u.Telephone,
                IsActive = u.IsActive,
                IsDeleted = u.IsDeleted
            };
        }
    }
}
