using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
   [Table("Categories")] 
    public class Category
    {
        public Category()
        {
            Symptoms = new Collection<Symptom>();
            Subcategories = new Collection<Subcategory>();
        }
        
        public int CategoryId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }

        public ICollection<Symptom> Symptoms { get; set; }
    }
}