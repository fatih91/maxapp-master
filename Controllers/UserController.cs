using System.Threading.Tasks;
using AutoMapper;
using maxapp.Controllers.Resources;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using HttpContextAccessorExtension;
using Microsoft.AspNetCore.Http;

namespace maxapp.Controllers
{
    
    [Route("/api/user")]
    public class UserController:  Controller
    {
        private readonly IMapper mapper;
        private readonly IUserRepository repository;
        private readonly IHttpContextAccessor httpContextAccessor;

        
        public UserController(IMapper mapper, IUserRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.httpContextAccessor = httpContextAccessor;
             
        }
        
        [HttpGet("diagnoses")]
        public async Task<IActionResult> GetDiagnosesByUser()
        {
            
            var currentUser = httpContextAccessor.CurrentUser();
            
            var user = await repository.GetUserWithDiagnoses(currentUser);
         
            
            if(user == null)
                return NotFound();
 
            var userResource = mapper.Map<User, UserDiagnoseResource>(user);

            return Ok(userResource); //diagnoseResource);
        }
        
        [HttpGet("symptoms")]
        public async Task<IActionResult> GetSymptomsByUser()
        {
            
            var currentUser = httpContextAccessor.CurrentUser();
            
            var user = await repository.GetUsersWithSymptoms(currentUser);
         
            
            if(user == null)
                return NotFound();
 
            var userResource = mapper.Map<User, UserSymptomResource>(user);
            
            return Ok(userResource); //diagnoseResource);
        }
    }

   
}