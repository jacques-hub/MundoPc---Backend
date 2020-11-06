using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Entities;
using Proyect.Interface;
using Proyect.Interface.DTOs;

namespace Project.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRepairController : ControllerBase
    {
        private readonly IProductRepairRepository _productRepairRepository;

        public ProductRepairController(IProductRepairRepository productRepairRepository)
        {
            _productRepairRepository = productRepairRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productRepair = await _productRepairRepository.GetAll();
            return Ok(productRepair.Where(x => !x.IsDeleted));
        }

        // GET: api/Product/5
        //[AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var productRepair = await _productRepairRepository.GetById(id);
            if (productRepair != null)
            {
                return Ok(productRepair);
            }
            else
            {
                return Ok("El Producto para Reparación no existe");
            }

        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create(ProductRepairDto productRepair)
        {
            if (productRepair == null)
                return BadRequest();

            var _productRepair = _productRepairRepository.GetAll().Result.
                FirstOrDefault(x => x.Code == productRepair.Code 
                && x.Description == productRepair.Description 
                && x.BrandId == productRepair.BrandId
                && x.CategoryId == productRepair.CategoryId);
            if (_productRepair != null)
            {
                ModelState.AddModelError("description", "La Producto Reparado ya existe");
                return BadRequest(ModelState);
            }

            await _productRepairRepository.Create(productRepair);
            return Ok(productRepair);

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ProductRepairDto productRepair)
        {
            var _productRepair = await _productRepairRepository.GetById(productRepair.Id);

            if (_productRepair != null)
            {
                await _productRepairRepository.Update(productRepair);
                return Ok(_productRepair);
            }
            else
            {
                return Ok("El Producto para Reparación no existe");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var _productRepairDelete = await _productRepairRepository.GetById(id);
            if (_productRepairDelete != null)
            {
                await _productRepairRepository.Delete(_productRepairDelete);
                return Ok(_productRepairDelete);
            }
            else
            {
                return Ok("El Producto para Reparación no existe");
            }
        }
    }
}