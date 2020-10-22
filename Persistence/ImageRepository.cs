using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace maxapp.Persistence
{
    public class ImageRepository : IImageRepository
    {
        private readonly MaxPanelContext context;
        
        public ImageRepository(MaxPanelContext context)
        {
            this.context = context;
        }
        

        public async Task<IEnumerable<Image>> GetImages(int symptomId)
        {
            return await context.Images
                .Where(
                    i => i.SymptomId == symptomId)
                .ToListAsync();
        }

        public async Task<Image> GetImage(string fileName)
        {
            return await context.Images.SingleOrDefaultAsync(i => i.FileName == fileName);
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Remove(Image image)
        {
            context.Remove(image);
        }
    }
}