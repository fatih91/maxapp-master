using System.Collections.Generic;
using System.Threading.Tasks;
using maxapp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using maxapp.Core.Interfaces;

namespace maxapp.Persistence
{
    public class DiagnoseRepository : IDiagnoseRepository
    {
        private readonly MaxPanelContext context;

        public DiagnoseRepository(MaxPanelContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Diagnose>> GetDiagnoses(Filter filter)
        {
            var query = context.Diagnoses
                .Include(d => d.Synonyms)
                .Include(d => d.Icds)
                .Include(d => d.Image)
                    .ThenInclude(i => i.Image)
                .Include(d => d.Differentialdiagnoses)
                .ThenInclude(df => df.DifferentialDiagnose)
                .AsQueryable();
                /*.Include(m => m.Prognoses)
                .Include(d => d.Differentialdiagnoses)
                    .ThenInclude(df => df.DifferentialDiagnose)
                .Include(m => m.Checklists)
                .Include(m => m.Therapies)
                .Include(m => m.Symptoms)
                    .ThenInclude(ds => ds.Symptom)
                                .ThenInclude(p => p.Images)
                .Include(m => m.Tags)
                    .ThenInclude(dta => dta.Tag)
                .AsQueryable();*/

            
            if (!String.IsNullOrEmpty(filter.SearchTerm) && String.IsNullOrEmpty(filter.Icd) && !filter.SearchTags.Any())
            {
                query = query.Where(d => d.TechnicalTerm.Contains(filter.SearchTerm) || d.Synonyms.Any(syn => syn.Name.Contains(filter.SearchTerm)));
            }
            
            if (String.IsNullOrEmpty(filter.SearchTerm) && !String.IsNullOrEmpty(filter.Icd) && !filter.SearchTags.Any())
            {
                query = query.Where(d => d.Icds.Any(icd => icd.Name.Contains(filter.Icd)));
            }
            
            if (!String.IsNullOrEmpty(filter.SearchTerm) && !String.IsNullOrEmpty(filter.Icd) && !filter.SearchTags.Any())
            {
                query = query.Where(d => d.Icds.Any(icd => icd.Name.Contains(filter.Icd)) && d.TechnicalTerm.Contains(filter.SearchTerm) || d.Synonyms.Any(syn => syn.Name.Contains(filter.SearchTerm)));
            }
            
            if (filter.SearchTags.Any() && String.IsNullOrEmpty(filter.SearchTerm) && String.IsNullOrEmpty(filter.Icd))
            {
                query = query.Where(d => filter.SearchTags.All(ft => d.Tags.Any(dta => ft.Equals(dta.Tag.Name))));
            }
            
            if(!String.IsNullOrEmpty(filter.SearchTerm) && !String.IsNullOrEmpty(filter.Icd) &&  filter.SearchTags.Any())
                query = query.Where(d => d.Icds.Any(icd => icd.Name.Contains(filter.Icd)) && (d.TechnicalTerm.Contains(filter.SearchTerm) || d.Synonyms.Any(syn => syn.Name.Contains(filter.SearchTerm))) && filter.SearchTags.All(ft => d.Tags.Any(t => ft.Equals(t.Tag.Name))));
            
            if(String.IsNullOrEmpty(filter.SearchTerm) && !String.IsNullOrEmpty(filter.Icd) &&  filter.SearchTags.Any())
                query = query.Where(d => (d.Icds.Any(icd => icd.Name.Contains(filter.Icd)) && filter.SearchTags.All(ft => d.Tags.Any(t => ft.Equals(t.Tag.Name)))));
            
            if(!String.IsNullOrEmpty(filter.SearchTerm) && String.IsNullOrEmpty(filter.Icd) &&  filter.SearchTags.Any())
                query = query.Where(d => (d.TechnicalTerm.Contains(filter.SearchTerm) || d.Synonyms.Any(syn => syn.Name.Contains(filter.SearchTerm))) && filter.SearchTags.All(ft => d.Tags.Any(t => ft.Equals(t.Tag.Name))));
            
            return await query.ToListAsync();
        }


        public async Task<Diagnose> GetDiagnose(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Diagnoses.FindAsync(id);

            return await context.Diagnoses
                .Include(d => d.Differentialdiagnoses)
                    .ThenInclude(df => df.DifferentialDiagnose)
                        .ThenInclude(df => df.Icds)
                 .Include(d => d.Differentialdiagnoses)
                     .ThenInclude(df => df.DifferentialDiagnose)
                        .ThenInclude(df => df.Synonyms)
                .Include(d => d.Checklists)
                .Include(d => d.Icds)
                .Include(d => d.Diagnostics)
                .Include(d => d.Synonyms)
                .Include(d => d.Prognoses)
                .Include(d => d.Therapies)
                .Include(m => m.Symptoms)
                    .ThenInclude(ds => ds.Symptom)
                                .ThenInclude(p => p.Images)
                .Include(m => m.Symptoms)
                    .ThenInclude(ds => ds.Symptom)
                                .ThenInclude(s => s.Synonyms)
                .Include(ta => ta.Tags)
                    .ThenInclude(t => t.Tag)
                .Include(d => d.Image)
                    .ThenInclude(di => di.Image)
                .SingleOrDefaultAsync(d => d.DiagnoseId == id);
        }
        
      /*  public async Task<IEnumerable<Diagnose>> GetDiagnose(Filter filter)
        {
           

            var query = context.Diagnoses
                .Include(d => d.DiagnoseId)
                .Include(d => d.Definition)
                .AsQueryable();
            
            
            return await query.ToListAsync();
        }*/

        public void Add(Diagnose diagnose)
        {
            context.Diagnoses.Add(diagnose);
        }

        public void Remove(Diagnose diagnose)
        {
            context.Remove(diagnose);
        }
        
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}