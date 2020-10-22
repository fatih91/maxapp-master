using System.Collections.Generic;
using System.Threading.Tasks;
using maxapp.Core.Models;

namespace maxapp.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserWithDiagnoses(string userId);
        Task<User> GetUsersWithSymptoms(string userId);
    }
}