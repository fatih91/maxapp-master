using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace maxapp.Persistence
{
    public class TagRepository : ITagRepository
    {
        private readonly MaxPanelContext context;

        public TagRepository(MaxPanelContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Tag>> GetTags(Filter filter)
        {
            var query = context.Tags
                .Include(ta => ta.Diagnoses)
                    .ThenInclude(d => d.Diagnose)
                .Include(ta => ta.Symptoms)
                    .ThenInclude(s => s.Symptom)
                .AsQueryable();

            //Vorher Searchstring bzw. SearchTerm?
                //query = query.Where(t => filter.SearchTags.Contains(t.Name));
            
            
            return await query.ToListAsync();
        }

        public async Task<Tag> GetTag(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Tags.FindAsync(id);

            return await context.Tags
                .Include(ta => ta.Diagnoses)
                    .ThenInclude(d => d.Diagnose)
                .Include(ta => ta.Symptoms)
                    .ThenInclude(s => s.Symptom)
                .SingleOrDefaultAsync(t => t.TagId == id);
        }

        public async Task<IEnumerable<Tag>> GetTagsFromIds(IEnumerable<int> tagIds)
        {

            var query = context.Tags.AsQueryable();
            
            query = query.Where(t => tagIds.Contains(t.TagId));
            
            
            return await query.ToListAsync();

        }
        
        
        
        public void Add(Tag tag)
        {
            context.Tags.Add(tag);
        }
        
        public void AddTags(ICollection<Tag> tags)
        {
            foreach (var tag in tags)
            {
                context.Tags.Add(tag);   
            }
        }

        public void Remove(Tag tag)
        {
            context.Tags.Remove(tag);
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}