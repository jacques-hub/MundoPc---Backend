namespace Project.ApiRest.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Project.Domain.Entities;
    using Proyect.Interface;
    using Proyect.Interface.DTOs;
    using System.Linq;
    using System.Threading.Tasks;

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
            
        //[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var product = await _productRepository.GetAll();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var porduct = await _productRepository.GetById(id);
            if (porduct != null)
            {
                return Ok(porduct);
            }
            else
            {
                return Ok("El Producto que busca no existe");
            }

        }


        // GET: api/Product/5
        //[AllowAnonymous]
        [HttpGet("withCode")]
        public async Task<IActionResult> SearchProduct(string code)
        {
            var products = await _productRepository.GetByCode(code);

            if (products == null)
                return BadRequest(new { message = "No hay productos que listar" });

            return Ok(products);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto product)
        {
            if (product == null)
                return BadRequest();

            var _product = _productRepository.GetAll().Result.FirstOrDefault(x => x.Code == product.Code);
            if (_product != null)
            {
                ModelState.AddModelError("codigo", "El código del producto ya existe");
                return BadRequest(ModelState);
            }

            await _productRepository.Create(product);
            return Ok(product);

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ProductDto product)
        {
            var _product = await _productRepository.GetById(product.Id);

            if (_product != null)
            {
                await _productRepository.Update(_product);
                return Ok(_product);
            }
            else
            {
                return Ok("El Producto no existe");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _productDelete = await _productRepository.GetById(id);
            if (_productDelete != null)
            {
                await _productRepository.Delete(id);
                return Ok(_productDelete);
            }
            else
            {
                return Ok("El Producto no Existe");
            }
        }

    }
}
