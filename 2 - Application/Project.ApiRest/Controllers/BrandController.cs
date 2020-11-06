namespace Project.ApiRest.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Project.Domain.Entities;
    using Proyect.Interface;

    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {

        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var brand = await _brandRepository.GetAll();
            return Ok(brand.Where(x => !x.IsDeleted));
        }

        // GET: api/Product/5
        //[AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandRepository.GetById(id);
            if (brand != null)
            {
                return Ok(brand);
            }
            else
            {
                return Ok("La marca no existe");
            }

        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (brand == null)
                return BadRequest();

            var _brand = _brandRepository.GetAll().Result.FirstOrDefault(x => x.Description == brand.Description);
            if (_brand != null)
            {
                ModelState.AddModelError("description", "La marca ya existe");
                return BadRequest(ModelState);
            }

            var newbrand = new Brand()
            {
                Description = brand.Description
            };
            await _brandRepository.Create(newbrand);
            return Ok(newbrand);

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Brand brand)
        {
            var _brand = await _brandRepository.GetById(id);

            if (_brand != null)
            {
                _brand.Description = brand.Description;
                await _brandRepository.Update(_brand);
                return Ok(_brand);
            }
            else
            {
                return Ok("La marca no existe");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _brandDelete = await _brandRepository.GetById(id);
            if (_brandDelete != null)
            {
                await _brandRepository.Delete(_brandDelete);
                return Ok(_brandDelete);
            }
            else
            {
                return Ok("La marca no existe");
            }
        }
    }
}