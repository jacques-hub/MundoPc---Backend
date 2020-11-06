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
    public class TechnicalServiceController : ControllerBase
    {
        private readonly ITechnicalServiceRepository _technicalServiceRepository;

        public TechnicalServiceController(ITechnicalServiceRepository technicalServiceRepository)
        {
            _technicalServiceRepository = technicalServiceRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var technicalService = await _technicalServiceRepository.GetAll();
            return Ok(technicalService.Where(x => !x.IsDeleted));
        }

        // GET: api/Product/5
        //[AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var technicalService = await _technicalServiceRepository.GetById(id);
            if (technicalService != null)
            {
                return Ok(technicalService);
            }
            else
            {
                return Ok("El Reporte de Servicio Técnico no existe");
            }

        }

        [HttpGet("withCode")]
        public async Task<IActionResult> SearchReport(string code)
        {
            var technicalService = await _technicalServiceRepository.GetByCode(code);
            if (technicalService != null)
            {
                return Ok(technicalService);
            }
            else
            {
                return Ok("El Reporte de Servicio Técnico no existe");
            }

        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Create(TechnicalServiceDto technicalService)
        {
            if (technicalService == null)
                return BadRequest();

            var _technicalService = _technicalServiceRepository.GetAll()
                .Result.FirstOrDefault(x => x.Total!= -1);//aca no se como hacer
            if (_technicalService != null)
            {
                ModelState.AddModelError("DateReceived", $"La Producto para Reparacion que ingreso el {_technicalService.DateReceived.ToShortDateString()}\n todavia no fué retirado");
                return BadRequest(ModelState);
            }

            await _technicalServiceRepository.Create( technicalService);
            return Ok( technicalService);

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(TechnicalServiceDto technicalService)
        {
            var _technicalService = await _technicalServiceRepository.GetById(technicalService.Id);

            if (_technicalService != null)
            {
                await _technicalServiceRepository.Update(_technicalService);
                return Ok(_technicalService);
            }
            else
            {
                return Ok("El Reporte de Servicio Técnico no existe");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var _technicalServiceDelete = await _technicalServiceRepository.GetById(id);
            if (_technicalServiceDelete != null)
            {
                await _technicalServiceRepository.Delete(_technicalServiceDelete);
                return Ok(_technicalServiceDelete);
            }
            else
            {
                return Ok("El Reporte de Servicio Técnico no existe");
            }
        }
    }
}