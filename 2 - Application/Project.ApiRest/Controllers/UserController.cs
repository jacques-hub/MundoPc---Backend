namespace Project.ApiRest.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Project.ApiRest.Models;
    using Project.Domain.Entities;
    using Project.Infraestructure;
    using Proyect.Interface;
    using Proyect.Interface.DTOs;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    //[Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        DataContext _context;

        public UserController(IUserRepository userRepository, DataContext context )
        {
            _userRepository = userRepository;
            _context = context;
        }

        //[AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel userParam)
        {
            var user = await _userRepository.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Email or Paswword is incorrect" });

            return Ok(user);
        }

        //[BasicAuth]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userRepository.GetAll();
            return Ok(user);
        }

        //get by email
        [HttpGet("withEmail")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userRepository.GetAll(); 
            var _user = user.FirstOrDefault(x => x.Email == email && x.IsDeleted != true);

            if (_user == null)
            {
                ModelState.AddModelError("email", "El usuario con ese email no existe");
                return BadRequest(ModelState);
            }
            await _userRepository.GetByEmail(email);
            return Ok(user);
        }

        // POST: api/Product
        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(UserDto user)
        {
            if (user == null)
                return BadRequest();

            var _user = _userRepository.GetAll().Result.FirstOrDefault(x => x.Email == user.Email && x.IsDeleted != true);
            if (_user != null)
            {
                ModelState.AddModelError("email", "El email del usuario ya existe");
                return BadRequest(ModelState);
            }
            await _userRepository.Create(user);
            return Ok(user);

        }
  

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UserDto user)
        {
            var _user = await _userRepository.GetById(user.Id);

            if (_user != null)
            {
                await _userRepository.Update(user);
                return Ok(user);
            }
            else
            {
                return Ok("User Not Exist");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _userDelete = await _userRepository.GetById(id);
            if (_userDelete != null)
            {
                await _userRepository.Delete(_userDelete);
                return Ok(_userDelete);
            }
            else
            {
                return Ok("User NotExist");
            }
        }
    }
    
}
