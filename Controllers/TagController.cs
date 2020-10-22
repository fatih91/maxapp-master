using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using maxapp.Controllers.Resources;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maxapp.Controllers
{
    
    [AllowAnonymous]
    [Route("api/tags")]
    public class TagController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITagRepository repository;

        public TagController(IMapper mapper, ITagRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await repository.GetTag(id);

            if (tag == null)
                return NotFound();

            var tagResource = mapper.Map<Tag, TagResource>(tag);
            
            return Ok(tagResource);
        }

        [HttpGet]//ToDo: Tags suchen mit einer Stringliste
        public async Task<IEnumerable<TagResource>> GetTags(FilterResource filterResource)
        {
            var filter = mapper.Map<FilterResource, Filter>(filterResource);
            var tags = await repository.GetTags(filter);

            return mapper.Map<IEnumerable<Tag>, IEnumerable<TagResource>>(tags);
        }
        
        

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] DSTagResource dsTagResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tag = mapper.Map<DSTagResource, Tag>(dsTagResource);

            repository.Add(tag);
            await repository.CompleteAsync();

            tag = await repository.GetTag(tag.TagId);

            var result = mapper.Map<Tag, TagResource>(tag);
            return Ok(result);
        }

        [HttpPost("{createtags}")]
        public async Task<IActionResult> CreateTags([FromBody] DSTagResource[] dsTagResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tags = mapper.Map<ICollection<DSTagResource>, ICollection<Tag>>(dsTagResource);

            repository.AddTags(tags);
            await repository.CompleteAsync();

            var ids = from t in tags
                select t.TagId;
            
            var resultTags = await repository.GetTagsFromIds(ids);

            var result = mapper.Map<IEnumerable<Tag>, IEnumerable<DSTagResource>>(resultTags);
            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] DSTagResource tagResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tag = await repository.GetTag(id);

            if (tag == null)
                return NotFound();

            mapper.Map<DSTagResource, Tag>(tagResource, tag);
            await repository.CompleteAsync();
            tag = await repository.GetTag(tag.TagId);
            var result = mapper.Map<Tag, TagResource>(tag);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await repository.GetTag(id, false);

            if (tag == null)
                return NotFound();
            
            repository.Remove(tag);
            await repository.CompleteAsync();
            return Ok(id);
        }

    }
}