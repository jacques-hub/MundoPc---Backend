namespace Project.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class EntityBase
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
