using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bootcamp_store_backend.Domain.Entities
{
    [Table("category")]
    public class Category
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName ="varchar(100)")]
        [MinLength(3)]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Column(TypeName ="varchar(2000)")]
        public string? Description { get; set; }

        [Required]
        public byte[] Image { get; set; }

    }
}