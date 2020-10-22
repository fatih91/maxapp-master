using System.Collections.Generic;
using System.Threading.Tasks;
using maxapp.Core.Models;

namespace maxapp.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Subcategory> GetParentCategory(int id, bool includeRelated = true);
        Task<Category> GetSubcategories(int id, bool includeRelated = true);
        Task<Category> GetCategory(int id, bool includeRelated = true, bool excludeSubcategories = false, bool excludeSymptoms = true);
        Task<IEnumerable<Category>> GetCategories();
        void Add(Subcategory subcategory);
        void Add(Category category);
        void Remove(Category category);
        void Remove(Subcategory category);
        Task CompleteAsync();
    }
}