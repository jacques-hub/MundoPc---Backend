using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities
{
    public class ProductRepair: EntityBase
    {
        public string Description { get; set; }
        public long BrandId { get; set; }
        public long CategoryId { get; set; }
        public string Code{ get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? SalePrice { get; set; }

        //propiedades de  navegacion
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<TechnicalService> TechnicalServices { get; set; }
    }
}
