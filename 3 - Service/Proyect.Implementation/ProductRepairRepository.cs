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

    public class ProductRepairRepository: IProductRepairRepository
    {
        private readonly IRepository<ProductRepair> _productRepairRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Category> _categoryRepository;

        DataContext _context;

        public ProductRepairRepository(IRepository<ProductRepair> productRepairRepository,
                                IRepository<Brand> brandRepository,
                                IRepository<Category> categoryRepository, DataContext context)
        {
            _productRepairRepository = productRepairRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public async Task Create(ProductRepairDto dto)
        {
            var _brand = await _brandRepository.GetById(dto.BrandId);
            if (_brand == null)
                throw new Exception("Por favor cargue alguna Marca antes de cargar un producto");

            var _category = await _categoryRepository.GetById(dto.CategoryId);
            if (_category == null)
                throw new Exception("Por favor cargue alguna Rubro antes de cargar un producto");

            var newProductRepair = new ProductRepair()
            {
                Description = dto.Description,
                BrandId = _brand.Id,
                CategoryId = _category.Id,
                Code = dto.Code,
                CostPrice = dto.CostPrice.HasValue?dto.CostPrice.Value:default,
                SalePrice =dto.SalePrice.HasValue?dto.SalePrice.Value:default,
                IsDeleted = false
            };
            await _productRepairRepository.Create(newProductRepair);
        }

        public async Task Delete(ProductRepairDto dto)
        {
            var e = await _productRepairRepository.GetById(dto.Id);
            await _productRepairRepository.Delete(e);
        }

        public async Task<IEnumerable<ProductRepairDto>> GetAll()
        {
            var productRepairAll =
                await _productRepairRepository.GetAll(null, null, false);

            return productRepairAll.Select(x => new ProductRepairDto()
            {
                Id = x.Id,
                Description = x.Description,
                IsDeleted = x.IsDeleted,
                BrandId = x.BrandId,
                BrandDescription = _brandRepository.GetById(x.BrandId).Result.Description,
                CategoryId = x.CategoryId,
                CategoryDescription = _categoryRepository.GetById(x.CategoryId).Result.Description,
                Code = x.Code,
                CostPrice = x.CostPrice,
                SalePrice = x.SalePrice

            }).Where(y => y.IsDeleted != true);
        }

        public async Task<ProductRepairDto> GetById(long UserId)
        {
            var x = await _productRepairRepository.GetById(UserId);
            return new ProductRepairDto()
            {
                Id = x.Id,
                Description = x.Description,
                IsDeleted = x.IsDeleted,
                BrandId = x.BrandId,
                BrandDescription = _brandRepository.GetById(x.BrandId).Result.Description,
                CategoryId = x.CategoryId,
                CategoryDescription = _categoryRepository.GetById(x.CategoryId).Result.Description,
                Code = x.Code,
                CostPrice = x.CostPrice,
                SalePrice = x.SalePrice
            };
        }

        public async Task Update(ProductRepairDto dto)
        {
            var _brand = await _brandRepository.GetById(dto.BrandId);
            if (_brand == null)
                throw new Exception("La Marca que introdujo no existe");

            var _category = await _categoryRepository.GetById(dto.CategoryId);
            if (_category == null)
                throw new Exception("La Categoría que introdujo no existe");

            var _p = await _productRepairRepository.GetById(dto.Id);
            _p.Code = dto.Code;
            _p.SalePrice = dto.SalePrice.HasValue ? dto.SalePrice.Value : default;
            _p.Description = dto.Description;
            _p.CostPrice = dto.CostPrice.HasValue ? dto.CostPrice.Value : default;
            _p.Brand = _brand;
            _p.Category = _category;

            await _productRepairRepository.Update(_p);
        }
    }
}
