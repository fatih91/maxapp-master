using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("Subcategories")]
    public class Subcategory
    {
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public Category Category { get; set; }
        public Category SubCategory { get; set; }
    }
}