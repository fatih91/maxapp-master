using System.Collections.Generic;
using System.Collections.ObjectModel;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class SaveCategoryResource
    {
        public string Name { get; set; }
        public int ParentId { get; set; } 
        public ICollection<int> Symptoms { get; set; }

        public SaveCategoryResource()
        {
            Symptoms = new Collection<int>();
        }
 
    }
}