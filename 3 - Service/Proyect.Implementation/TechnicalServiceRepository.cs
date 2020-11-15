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
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.Transactions;

    public class TechnicalServiceRepository: ITechnicalServiceRepository
    {
        private readonly IRepository<TechnicalService> _technicalServiceRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<ProductRepair> _productRepairRepository;
        DataContext _context;

        public TechnicalServiceRepository(IRepository<TechnicalService> technicalServiceRepository,
                                           IRepository<User> userRepository,
                                           IRepository<ProductRepair> productRepairRepository,
                                           DataContext context)
        {
            _technicalServiceRepository = technicalServiceRepository;
            _userRepository = userRepository;
            _productRepairRepository = productRepairRepository;
            _context = context;
        }

        public async Task Create(TechnicalServiceDto dto)
        {
            var _user = await _userRepository.GetById(dto.UserId);
            if (_user == null)
                throw new Exception("El Usuario ingresado no existe");

            var _p = await _productRepairRepository.GetAll();
            if (_p.FirstOrDefault(x=>x.Code == dto.ProductRepairCode) != null){
                throw new Exception("El producto ya fue cargado");
            }

            await _productRepairRepository.Create(new ProductRepair()
            {
                CategoryId = dto.ProductRepairCategoryId,
                BrandId = dto.ProductRepairBrandId,
                Description = dto.ProductRepairDescription,
                Code = dto.ProductRepairCode
            });

            var _prod = await _productRepairRepository.GetAll(null, null, false);
            var prod = _prod.FirstOrDefault(x => x.Code == dto.ProductRepairCode);
            //System.Console.WriteLine(_productId);
            var newTechnicalService = new TechnicalService()
            {
                SerialNumber = dto.SerialNumber,
                Observations = dto.Observations,
                AccessoriesReceived = dto.AccessoriesReceived,
                EquipmentFailure = dto.EquipmentFailure,
                DateReceived = dto.DateReceived,
                ServiceStatus = dto.ServiceStatus,
                DateStatus = dto.DateStatus,
                UserId = _user.Id,
                TotalInputs = dto.TotalInputs, //total insumos
                TotalLabor = dto.TotalLabor,//mano de obra
                Total = dto.Total,
                Diagnostic = dto.Diagnostic,
                IsDeleted = false,
                ProductRepairId = prod.Id
            };

            await _technicalServiceRepository.Create(newTechnicalService);
          
            
        }

        public async Task Delete(TechnicalServiceDto dto)
        {
            var e = await _technicalServiceRepository.GetById(dto.Id);
            await _technicalServiceRepository.Delete(e);
        }

        public async Task<IEnumerable<TechnicalServiceDto>> GetAll()
        {
            var technicalServiceAll =
                await _technicalServiceRepository.GetAll(null, null, false);

            return technicalServiceAll.Select(x => new TechnicalServiceDto()
            {
                Id = x.Id,
                SerialNumber = x.SerialNumber,
                Observations = x.Observations,
                AccessoriesReceived = x.AccessoriesReceived,
                EquipmentFailure = x.EquipmentFailure,
                DateReceived = x.DateReceived,
                ServiceStatus = x.ServiceStatus,
                DateStatus = x.DateStatus,
                UserId = x.UserId,
                UserName = _userRepository.GetById(x.UserId).Result.FirstName, //fullName
                ProductRepairId = x.ProductRepairId,
                ProductRepairDescription = _productRepairRepository.GetById(x.ProductRepairId).Result.Description,
                TotalInputs = x.TotalInputs, //total insumos
                TotalLabor = x.TotalLabor,//mano de obra
                Total = x.Total,
                Diagnostic = x.Diagnostic,
                IsDeleted = x.IsDeleted
            }).Where(y => y.IsDeleted != true);//.ToList();
        }

        public async Task<TechnicalServiceDto> GetByCode(string code)
        {
            var technicalServiceAll =
                await _technicalServiceRepository.GetAll(null, null, false);

            var x = technicalServiceAll.FirstOrDefault(t => t.SerialNumber == code);
            if (x == null) return null; 
                
                //throw new Exception("no se encontró ningún Reporte con el código ingresado");

            return new TechnicalServiceDto()
            {
                Id = x.Id,
                SerialNumber = x.SerialNumber,
                Observations = x.Observations,
                AccessoriesReceived = x.AccessoriesReceived,
                EquipmentFailure = x.EquipmentFailure,
                DateReceived = x.DateReceived,
                ServiceStatus = x.ServiceStatus,
                DateStatus = x.DateStatus,
                UserId = x.UserId,
                UserName = _userRepository.GetById(x.UserId).Result.FirstName, //fullName
                ProductRepairId = x.ProductRepairId,
                ProductRepairDescription = _productRepairRepository.GetById(x.ProductRepairId).Result.Description,
                TotalInputs = x.TotalInputs, //total insumos
                TotalLabor = x.TotalLabor,//mano de obra
                Total = x.Total,
                Diagnostic = x.Diagnostic,
                IsDeleted = x.IsDeleted
            };
        }

        public async Task<TechnicalServiceDto> GetById(long id)
        {
            var x = await _technicalServiceRepository.GetById(id);
            if (x == null)
                throw new Exception("Ningun producto tiene el id que busca");

            return new TechnicalServiceDto()
            {
                Id = x.Id,
                SerialNumber = x.SerialNumber,
                Observations = x.Observations,
                AccessoriesReceived = x.AccessoriesReceived,
                EquipmentFailure = x.EquipmentFailure,
                DateReceived = x.DateReceived,
                ServiceStatus = x.ServiceStatus,
                DateStatus = x.DateStatus,
                UserId = x.UserId,
                UserName = _userRepository.GetById(x.UserId).Result.FirstName, //fullName
                ProductRepairId = x.ProductRepairId,
                ProductRepairDescription = _productRepairRepository.GetById(x.ProductRepairId).Result.Description,
                TotalInputs = x.TotalInputs, //total insumos
                TotalLabor = x.TotalLabor,//mano de obra
                Total = x.Total,
                Diagnostic = x.Diagnostic,
                IsDeleted = x.IsDeleted
            };
        }

        public async Task Update(TechnicalServiceDto dto)
        {
            var _t = await _technicalServiceRepository.GetById(dto.Id);
            _t.Observations = dto.Observations;
            _t.EquipmentFailure = dto.EquipmentFailure;
            _t.ServiceStatus = dto.ServiceStatus;
            _t.DateStatus = dto.DateStatus;
            _t.TotalInputs = dto.TotalInputs; //total insumos
            _t.TotalLabor = dto.TotalLabor;//mano de obra
            _t.Total = dto.Total;
            _t.Diagnostic = dto.Diagnostic;


            await _technicalServiceRepository.Update(_t);
        }
    }
}
