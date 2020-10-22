using System.Collections.Generic;
using System.Collections.ObjectModel;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class CategoryResource
    {
        public CategoryResource()
        {
            Subcategories = new Collection<SubcategoryResource>();
            Symptoms = new Collection<CategorySymptomResource>();
        }        
        
        
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<SubcategoryResource> Subcategories { get; set; }
        public ICollection<CategorySymptomResource> Symptoms { get; set; }
    }
}