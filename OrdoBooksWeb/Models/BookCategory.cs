using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrdoBooksWeb.Models
{
    public class BookCategory
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100)]
        public int DisplayOrder { get; set; }





    }
}
