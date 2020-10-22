using System.Collections.Generic;
using System.Threading.Tasks;
using maxapp.Controllers.Resources;
using maxapp.Core.Models;

namespace maxapp.Core.Interfaces
{
    public interface ISymptomRepository
    {
        Task<IEnumerable<Symptom>> GetSymptoms(Filter filter); 
        Task<ICollection<Symptom>> GetSymptoms(ICollection<int> symptoms); 
        Task<Symptom> GetSymptom(int id, bool includeRelated = true); 
        void Add(Symptom symptom);
        void Remove(Symptom symptom);
        Task CompleteAsync();
        
    }
}