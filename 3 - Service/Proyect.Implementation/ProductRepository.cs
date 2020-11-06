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
    using System.Transactions;

    public class ProductRepository : IProductRepository
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Category> _categoryRepository;
        DataContext _context;

        public ProductRepository(IRepository<Product> productRepository, 
                                IRepository<Brand> brandRepository,
                                IRepository<Category> categoryRepository,
                                DataContext context)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public async Task Create(ProductDto dto)
        {
            var _brand = await _brandRepository.GetById(dto.BrandId);
            if (_brand == null)
                throw new Exception("Por favor cargue alguna Marca antes de cargar un producto");

            var _category = await _categoryRepository.GetById(dto.CategoryId);
            if (_category == null)
                throw new Exception("Por favor cargue alguna Rubro antes de cargar un producto");

            try
            {
                var newProduct = new Product()
                {
                    Image = dto.Image,
                    Code = dto.Code,
                    Name = dto.Name,
                    Description = dto.Description,
                    CostPrice = dto.CostPrice,
                    Aliquot = dto.Aliquot,
                    Stock = dto.Stock,
                    IsDeleted = false,
                    BrandId = _brand.Id,
                    CategoryId = _category.Id
                };
                await _productRepository.Create(newProduct);
            }
            catch (Exception e)
            {
                throw new Exception($"Al crear el Producto se produjo el error: {e.Message}");
            }               
        }

        public async Task Delete(long id)
        {
            var e = await _productRepository.GetById(id);
            await _productRepository.Delete(e);
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var productAll =
                await _productRepository.GetAll(null, null, false);

            return productAll.Select(x => new ProductDto()
            {
                Id = x.Id,
                Image = x.Image,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                IsDeleted = x.IsDeleted,
                Price = x.Aliquot * x.CostPrice,//se puede mejorar
                Stock = x.Stock,
                BrandId =x.BrandId,
                BrandDescription = _brandRepository.GetById(x.BrandId).Result.Description,
                CategoryId = x.CategoryId,
                CategoryDescription=_categoryRepository.GetById(x.CategoryId).Result.Description

            }).Where(y => y.IsDeleted != true);
              //.ToList(); 
        }

        public async Task<IEnumerable<ProductDto>> GetByCode(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                return await this.GetAll();
            }

            var productAll =
                await _productRepository.GetAll(null, null, false);

            return productAll.Select(x => new ProductDto()
            {
                Id = x.Id,
                Image = x.Image,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                IsDeleted = x.IsDeleted,
                Price = x.Aliquot * x.CostPrice,//se puede mejorar
                Stock = x.Stock,
                BrandId = x.BrandId,
                BrandDescription = _brandRepository.GetById(x.BrandId).Result.Description,
                CategoryId = x.CategoryId,
                CategoryDescription = _categoryRepository.GetById(x.CategoryId).Result.Description
            }).Where(y => y.IsDeleted != true &&
                    y.Code.Contains(code) || y.Name.Contains(code) ||
                    y.Description.Contains(code));
        }

        public async Task<ProductDto> GetById(long Id)
        {
            var x = await _productRepository.GetById(Id);
            //if (x == null)
              //  throw new Exception("Ningun producto tiene el id que busca");
            return new ProductDto()
            {
                Id = x.Id,
                Image = x.Image,
                Code = x.Code,
                Name = x.Name,
                Description = x.Description,
                IsDeleted = x.IsDeleted,
                Price = x.Aliquot * x.CostPrice,
                Stock = x.Stock,
                BrandId = x.BrandId,
                BrandDescription = _brandRepository.GetById(x.BrandId).Result.Description,
                CategoryId = x.CategoryId,
                CategoryDescription = _categoryRepository.GetById(x.CategoryId).Result.Description
            };
        }

        public async Task Update(ProductDto dto)
        {
            var _brand = await _brandRepository.GetById(dto.BrandId);
            if (_brand == null)
                throw new Exception("La Marca que introdujo no existe");

            var _category = await _categoryRepository.GetById(dto.CategoryId);
            if (_category == null)
                throw new Exception("La Categoría que introdujo no existe");

            var _p = await _productRepository.GetById(dto.Id);
            _p.Image = dto.Image;
            _p.Code = dto.Code;
            _p.Name = dto.Name;
            _p.Description = dto.Description;
            _p.CostPrice = dto.CostPrice;
            _p.Aliquot = dto.Aliquot;
            _p.Stock = dto.Stock;
            _p.BrandId = dto.BrandId;
            _p.CategoryId = dto.CategoryId;

            await _productRepository.Update(_p);
        }
    }
}




