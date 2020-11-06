using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities
{
    public class Product : EntityBase
    {
        public byte[] Image { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Aliquot { get; set; }
        public int Stock { get; set; }


        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }
    }
}
