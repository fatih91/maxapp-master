using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HttpContextAccessorExtension;
using maxapp.Controllers.Resources;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace maxapp.Controllers
{
  //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, )]
  //  [AllowAnonymous]
    [Route("/api/symptoms")]
    public class SymptomController: Controller
    {
        private readonly IMapper mapper;
        private readonly ISymptomRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SymptomController(IMapper mapper, ISymptomRepository repository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<SymptomResource>> GetSymptoms(FilterResource filterResource)
        {
            var filter = mapper.Map<FilterResource, Filter>(filterResource);
            var symptoms = await repository.GetSymptoms(filter);

            return mapper.Map<IEnumerable<Symptom>, IEnumerable<SymptomResource>>(symptoms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSymptom(int id)
        {
            var symptom = await repository.GetSymptom(id);

            if (symptom == null)
                return NotFound();

            var symptomResource = mapper.Map<Symptom, SymptomResource>(symptom);

            return Ok(symptomResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSymptom(int id)
        {
            var symptom = await repository.GetSymptom(id, includeRelated: false);

            if (symptom == null)
                return NotFound();
            
            repository.Remove(symptom);
            await unitOfWork.CompleteAsync();
            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSymptom([FromBody] SaveSymptomResource symptomResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            symptomResource.UserId = httpContextAccessor.CurrentUser();
            symptomResource.Modification = Modification.Created;
            
            var symptom = mapper.Map<SaveSymptomResource, Symptom>(symptomResource);
            
            repository.Add(symptom);
            await unitOfWork.CompleteAsync();

            symptom = await repository.GetSymptom(symptom.SymptomId);

            var result = mapper.Map<Symptom, SymptomResource>(symptom);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSymptom(int id, [FromBody] SaveSymptomResource symptomResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            symptomResource.UserId = httpContextAccessor.CurrentUser();
            symptomResource.Modification = Modification.Edited;

            var symptom = await repository.GetSymptom(id);

            if (symptom == null)
                return NotFound();

            mapper.Map<SaveSymptomResource, Symptom>(symptomResource, symptom);

            await repository.CompleteAsync();
            symptom = await repository.GetSymptom(symptom.SymptomId);
            var result = mapper.Map<Symptom, SymptomResource>(symptom);
            return Ok(result);
        }
    }
}