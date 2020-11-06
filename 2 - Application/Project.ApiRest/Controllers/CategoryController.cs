namespace Project.ApiRest.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Project.Domain.Entities;
    using Proyect.Interface;

    //Category controller _ add -> MVC controller
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var category = await _categoryRepository.GetAll();
            return Ok(category.Where(x => !x.IsDeleted));
        }

        // GET: api/Product/5
        //[AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return Ok("La categoía no existe");
            }

        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (category == null)
                return BadRequest();

            var _category = _categoryRepository.GetAll().Result.FirstOrDefault(x => x.Description == category.Description);
            if (_category != null)
            {
                ModelState.AddModelError("description", "La categoría ya existe");
                return BadRequest(ModelState);
            }

            var newCategory = new Category()
            {
                Description = category.Description
            };
            await _categoryRepository.Create(newCategory);
            return Ok(newCategory);

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Category category)
        {
            var _category = await _categoryRepository.GetById(id);

            if (_category != null)
            {
                _category.Description = category.Description;
                await _categoryRepository.Update(_category);
                return Ok(_category);
            }
            else
            {
                return Ok("La categoría no existe");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _categoryDelete = await _categoryRepository.GetById(id);
            if (_categoryDelete != null)
            {
                await _categoryRepository.Delete(_categoryDelete);
                return Ok(_categoryDelete);
            }
            else
            {
                return Ok("La categoría no existe");
            }
        }
    }
}