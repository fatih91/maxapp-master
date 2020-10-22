using System.Collections.Generic;
using System.Threading.Tasks;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace maxapp.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly MaxPanelContext context;

        public CategoryRepository(MaxPanelContext context)
        {
            this.context = context;
        }
        
        public async Task<Subcategory> GetParentCategory(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Subcategories.SingleOrDefaultAsync(sc => sc.SubcategoryId == id);

            return await context.Subcategories
                .Include(sc => sc.Category)
                .SingleOrDefaultAsync(sc => sc.SubcategoryId == id);

        }

        public async Task<Category> GetCategory(int id, bool includeRelated = true, bool excludeSubcategories = false, bool excludeSymptoms = true)
        {
            if (!includeRelated)
                return await context.Categories.FindAsync(id);


            if (excludeSubcategories)
                return await context.Categories
                    .Include(c => c.Symptoms)
                    .SingleOrDefaultAsync(c => c.CategoryId == id);

            if (!excludeSymptoms)
                return await context.Categories
                    .Include(c => c.Subcategories)
                     .ThenInclude(sc => sc.SubCategory)
                     .SingleOrDefaultAsync(c => c.CategoryId == id);
            
            return await context.Categories
                .Include(c => c.Symptoms)
                    .ThenInclude(s => s.Images)
                .Include(c => c.Symptoms)
                    .ThenInclude(s => s.Synonyms)
                .Include(c => c.Subcategories)
                    .ThenInclude(sc => sc.SubCategory)
               
                .SingleOrDefaultAsync(c => c.CategoryId == id);

        }
        
        
        public async Task<Category> GetSubcategories(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Categories.FindAsync(id);

            return await context.Categories
                .Include(c => c.Subcategories)
                .ThenInclude(sc => sc.SubCategory)
                .SingleOrDefaultAsync(c => c.CategoryId == id);

        }


        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Categories
                .Include(c => c.Subcategories)
                .ToListAsync();
        }
        
        public void Add(Subcategory subcategory)
        {
           context.Subcategories.Add(subcategory);
        }

        public void Add(Category category)
        {
            context.Categories.Add(category);
        }

        public void Remove(Category category)
        {
            context.Categories.Remove(category);
        }
        
        public void Remove(Subcategory category)
        {
            context.Subcategories.Remove(category);
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
