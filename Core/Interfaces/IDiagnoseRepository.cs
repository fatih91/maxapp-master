using System.Collections.Generic;
using System.Threading.Tasks;
using maxapp.Core.Models;

namespace maxapp.Core.Interfaces
{
    public interface IDiagnoseRepository
    {
        Task<IEnumerable<Diagnose>> GetDiagnoses(Filter filter); 
        Task<Diagnose> GetDiagnose(int id, bool includeRelated = true); 
        void Add(Diagnose diagnose);
        void Remove(Diagnose diagnose);
        Task CompleteAsync();
    }
}