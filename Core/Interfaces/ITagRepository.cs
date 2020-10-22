using System.Collections.Generic;
using System.Threading.Tasks;
using maxapp.Core.Models;

namespace maxapp.Core.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetTags(Filter filter);
        Task<IEnumerable<Tag>> GetTagsFromIds(IEnumerable<int> ids);
        Task<Tag> GetTag(int id, bool includeRelated = true);
        void Add(Tag tag);
        void AddTags(ICollection<Tag> tags);
        void Remove(Tag tag);
        Task CompleteAsync();
    }
}