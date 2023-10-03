using System.ComponentModel.DataAnnotations;

namespace OrdoBooksWeb.Models
{
    public class BookCategory
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }





    }
}
