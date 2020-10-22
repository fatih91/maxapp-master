using System.Threading.Tasks;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace maxapp.Persistence
{
    public class UserRepository : IUserRepository
    {
        
        private readonly MaxPanelContext context;

        public UserRepository(MaxPanelContext context)
        {
            this.context = context;
        }
        
        public async Task<User> GetUserWithDiagnoses(string userId)
        {
            return await context.Users
                .Include(u => u.Diagnoses)
                 .ThenInclude(d => d.Diagnose)
                    .ThenInclude(d => d.Synonyms)
                .Include(u => u.Diagnoses)
                .ThenInclude(d => d.Diagnose)
                     .ThenInclude(d => d.Image)
                        .ThenInclude(i => i.Image)
                .SingleOrDefaultAsync(u => u.Id == userId);
        }
        
        public async Task<User> GetUsersWithSymptoms(string userId)
        {
            return await context.Users
                .Include(u => u.Symptoms)
                    .ThenInclude(d => d.Symptom)
                        .ThenInclude(d => d.Synonyms)
                .Include(u => u.Symptoms)
                    .ThenInclude(d => d.Symptom)
                        .ThenInclude(d => d.Images)
                .SingleOrDefaultAsync(u => u.Id == userId);
        }
    }
}