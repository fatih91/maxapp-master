using System.Collections.Generic;
using System.Collections.ObjectModel;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class MapCategoryResource
    {
        public MapCategoryResource()
        {
            Subcategories = new Collection<SubcategoryResource>();
        }        
        
        
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public ICollection<SubcategoryResource> Subcategories { get; set; }
    }
}