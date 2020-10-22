using System.Collections.Generic;
using System.Threading.Tasks;
using maxapp.Core.Models;

namespace maxapp.Core.Interfaces
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetImages(int patientId);
        Task<Image> GetImage(string fileName);
        Task CompleteAsync();
        void Remove(Image image);
    }
}