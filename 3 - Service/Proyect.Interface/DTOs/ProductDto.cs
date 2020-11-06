using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyect.Interface.DTOs
{
    public class ProductDto: BaseDto
    {
        public byte[] Image { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public string BrandDescription { get; set; }
        public string CategoryDescription { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Aliquot { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

    }
}
