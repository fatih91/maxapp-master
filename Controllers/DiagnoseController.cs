using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using maxapp.Controllers.Resources;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HttpContextAccessorExtension;

namespace maxapp.Controllers
{
    [Route("/api/diagnoses")]
    public class DiagnoseController : Controller
    {
        private readonly IMapper mapper;
        private readonly IDiagnoseRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        
        
        public DiagnoseController(IMapper mapper, IDiagnoseRepository repository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<DiagnoseResource>> GetDiagnoses(FilterResource filterResource)
        {
            var filter = mapper.Map<FilterResource, Filter>(filterResource);
            var diagnoses = await repository.GetDiagnoses(filter);
            
            return mapper.Map<IEnumerable<Diagnose>, IEnumerable<DiagnoseResource>>(diagnoses);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateDiagnose([FromBody] SaveDiagnoseResource diagnoseResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            diagnoseResource.UserId = httpContextAccessor.CurrentUser();
            diagnoseResource.Modification = Modification.Created;
                
            
            var diagnose = mapper.Map<SaveDiagnoseResource, Diagnose>(diagnoseResource);
            
            repository.Add(diagnose);
            await repository.CompleteAsync();

            diagnose = await repository.GetDiagnose(diagnose.DiagnoseId);
            
            var result = mapper.Map<Diagnose, DiagnoseResource>(diagnose);
            return Ok(result);
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiagnose(int id, [FromBody] SaveDiagnoseResource diagnoseResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            diagnoseResource.UserId = httpContextAccessor.CurrentUser();
            diagnoseResource.Modification = Modification.Edited;

            var diagnose = await repository.GetDiagnose(id);
            
            if(diagnose == null)
                return NotFound();

            mapper.Map<SaveDiagnoseResource, Diagnose>(diagnoseResource, diagnose);
            
            await repository.CompleteAsync();
            diagnose = await repository.GetDiagnose(diagnose.DiagnoseId);
            var result = mapper.Map<Diagnose, DiagnoseResource>(diagnose);
            return Ok(result);
        }
     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnose(int id)
        {
            var diagnose = await repository.GetDiagnose(id, includeRelated: false);
            
            if(diagnose == null)
                return NotFound();
            
            repository.Remove(diagnose);
            await unitOfWork.CompleteAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnose(int id)
        {
            var diagnose = await repository.GetDiagnose(id);
            
            if(diagnose == null)
                return NotFound();

            var diagnoseResource = mapper.Map<Diagnose, DiagnoseResource>(diagnose);

            return Ok(diagnoseResource);
        }     
    }
}