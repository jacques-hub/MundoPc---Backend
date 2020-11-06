using System.Collections.Generic;

namespace Project.Domain.Entities
{
    public class Brand: EntityBase
    {
        public string Description { get; set; }


        //propiedades de navegacion-----------------------
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductRepair> ProductRepairs { get; set; }
    }
}
