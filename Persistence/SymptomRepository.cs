using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace maxapp.Persistence
{
    public class SymptomRepository : ISymptomRepository
    {
        private readonly MaxPanelContext context;

        public SymptomRepository(MaxPanelContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Symptom>> GetSymptoms(Filter filter)
        {
            var query = context.Symptoms
                .Include(s => s.Images)
                .Include(s => s.Diagnoses)
                .ThenInclude(d => d.Diagnose)
                .Include(s => s.Synonyms)
                .Include(ta => ta.Tags)
                .ThenInclude(t => t.Tag)
                .Include(s => s.Category)
                .AsQueryable();


            if (!String.IsNullOrEmpty(filter.SearchTerm) && String.IsNullOrEmpty(filter.Icd) && !filter.SearchTags.Any())
            {
                query = query.Where(s =>
                    s.TechnicalTerm.Contains(filter.SearchTerm) || s.Synonyms.Any(ss => ss.Name.Contains(filter.SearchTerm)));
            }
          
            if (String.IsNullOrEmpty(filter.SearchTerm) && filter.SearchTags.Any())
            {
                query = query.Where(s =>
                    filter.SearchTags.All(f => s.Tags.Any(sta => f.Equals(sta.Tag.Name))));
            }

            if (!String.IsNullOrEmpty(filter.SearchTerm) && filter.SearchTerm.Any())
            {//s.Term.Contains(filter.SearchTerm)
                query = query.Where(s =>
                    (s.TechnicalTerm.Contains(filter.SearchTerm) 
                     || s.Synonyms.Any(ss => ss.Name.Contains(filter.SearchTerm)) &&
                    filter.SearchTags.All(f => s.Tags.Any(sta => f.Equals(sta.Tag.Name)))));
            }

            return await query.ToListAsync();

        }

        public async Task<ICollection<Symptom>> GetSymptoms(ICollection<int> symptoms)
        {
            var query = context.Symptoms
                .Include(s => s.Images)
                .Include(s => s.Diagnoses)
                .ThenInclude(d => d.Diagnose)
                .Include(s => s.Synonyms)
                .Include(ta => ta.Tags)
                .ThenInclude(t => t.Tag)
                .AsQueryable();

            query = query.Where(s => 
                symptoms.Contains(s.SymptomId));

            return await query.ToListAsync();
        }


        public async Task<Symptom> GetSymptom(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Symptoms.FindAsync(id);

            return await context.Symptoms  
                .Include(s => s.Images)
                .Include(s => s.Category)
                .Include(s => s.Synonyms)
                .Include(s => s.Diagnoses)
                    .ThenInclude(ds => ds.Diagnose)
                        .ThenInclude(d => d.Icds)
                .Include(s => s.Diagnoses)
                    .ThenInclude(ds => ds.Diagnose)
                        .ThenInclude(d => d.Synonyms)
                .Include(ta => ta.Tags)
                    .ThenInclude(t => t.Tag)
                .SingleOrDefaultAsync(s => s.SymptomId == id);
        }

        public void Add(Symptom symptom)
        {
            context.Symptoms.Add(symptom);
        }

        public void Remove(Symptom symptom)
        {
            context.Remove(symptom);
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}