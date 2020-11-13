namespace Project.ApiRest.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Project.Domain.Entities;
    using Proyect.Interface;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class MessengerController : ControllerBase
    {
        private readonly IMessengerRepository _messengerRepository;

        public MessengerController(IMessengerRepository messengerRepository)
        {
            _messengerRepository = messengerRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var messenger = await _messengerRepository.GetAll();
            return Ok(messenger);
        }

        // GET: api/Product/5
        //[AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var messenger = await _messengerRepository.GetById(id);
            if (messenger != null)
            {
                return Ok(messenger);
            }
            else
            {
                return Ok("La mensaje no existe");
            }

        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create(Messenger messenger)
        {
            if (messenger == null)
                return BadRequest();

            var newMessage = new Messenger()
            {
                name = messenger.name,
                email = messenger.email,
                phone = messenger.phone,
                query = messenger.query,
                isAnswered = false,
                IsDeleted = false
            };
            await _messengerRepository.Create(newMessage);
            return Ok(newMessage);

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Messenger messenger)
        {
            var _messenger = await _messengerRepository.GetById(id);

            if (_messenger != null)
            {
                _messenger.name = messenger.name;
                _messenger.email = messenger.email;
                _messenger.query = messenger.query;
                _messenger.phone = messenger.phone;

                await _messengerRepository.Update(_messenger);
                return Ok(_messenger);
            }
            else
            {
                return Ok("La mensaje no existe");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _messengerDelete = await _messengerRepository.GetById(id);
            if (_messengerDelete != null)
            {
                await _messengerRepository.Delete(_messengerDelete);
                return Ok(_messengerDelete);
            }
            else
            {
                return Ok("La mensaje no existe");
            }
        }
    }
}