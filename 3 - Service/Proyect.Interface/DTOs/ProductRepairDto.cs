using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyect.Interface.DTOs
{
    public class ProductRepairDto: BaseDto
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? SalePrice { get; set; }

        //propiedades de  navegacion
        public long BrandId { get; set; }
        public string BrandDescription { get; set; }
        public long CategoryId { get; set; }
        public string CategoryDescription { get; set; }

    }
}
