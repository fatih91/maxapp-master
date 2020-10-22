using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using maxapp.Controllers.Resources;
using maxapp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace maxapp.Controllers
{
    [AllowAnonymous]
    [Route("/api/accounts")]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
     
        [AllowAnonymous]
        [HttpPost("login/")]
        public async Task<object> Login([FromBody] LoginResource login)
        {
         //   var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            var result =  await userManager.CheckPasswordAsync(userManager.Users.SingleOrDefault(r => r.Email == login.Email),
                    login.Password);
            if (result)
            {
                var appUser = userManager.Users.SingleOrDefault(r => r.Email == login.Email);
                return await GenerateJwtToken(login.Email, appUser);
            }
            
            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
            
            /*
             * nutze von Novell das signin, wenn novell die Logindaten akzeptiert, dann wird ein true zurückgegeben. Dies wird in einer if-Abfrage überprüft, falls true, dann wird nach der Stu gesucht, ob diese schon in der Nutzerdatenbank eingetragen ist. Falls es der Fall ist, müssen dann nur noch die userdetails in einen jwttoken generiert werden. Falls in der Nutzerdatenbank kein Nutzer diesbezüglich angelegt ist, wird einer neu angelegt. 
             */
        }
        
       [AllowAnonymous]
       [HttpPost("register/")]
        public async Task<object> Register([FromBody] RegisterResource register)
       {
           var test = new Collection<string>();
           
           test.Add("Extern");
           
           
            var user = new User
            {
                UserName = register.Email, 
                Email = register.Email
            };
            var result = await userManager.CreateAsync(user, register.Password);
            
            if (result.Succeeded)
            {
                var identyUser = await userManager.FindByNameAsync(register.Email);

                if (!String.IsNullOrEmpty(register.Role))
                {
                    var roleUpper  = register.Role.ToUpper();
                    if (await roleManager.RoleExistsAsync(roleUpper))
                    {
                        test.Add(roleUpper);
                        //await userManager.AddToRoleAsync(identyUser, model.Role);
                    }
                    else
                    {
                        return NotFound();
                    }
                }

                await userManager.AddToRolesAsync(identyUser, test);
       

                return Ok();
            }

            throw new ApplicationException(result.ToString());
        }
        
        

        [HttpPost("create")]
        public async Task<IActionResult> CreateRoles([FromBody] RoleRessource roleRessource)
        {

            if (String.IsNullOrEmpty(roleRessource.Role))
                return BadRequest();
            
            if(await roleManager.RoleExistsAsync(roleRessource.Role))
                throw new ApplicationException(roleRessource.Role + " already exists.");
            
            
            
            var role = new IdentityRole();
            role.Name = roleRessource.Role;

            await roleManager.CreateAsync(role);


            return Ok();
        }

        [HttpPost("adduserrole")]
        public async Task<IActionResult> AddRoletoUser([FromBody] UserRoleRessource userRoleResource)
        {

            
            if (String.IsNullOrEmpty(userRoleResource.Role))
                return BadRequest();

            if (!await roleManager.RoleExistsAsync(userRoleResource.Role))
                return NotFound(roleManager.RoleExistsAsync(userRoleResource.Role));

            var user = await userManager.FindByNameAsync(userRoleResource.Name);
            if (user == null)
                return BadRequest();

            var result = await userManager.AddToRoleAsync(user, userRoleResource.Role);
           
            return Ok(await userManager.GetRolesAsync(user));


        }
        
        [HttpDelete("deleteuserrole")]
        public async Task<IActionResult> DeleteRoletoUser([FromBody] UserRoleRessource userRoleResource)
        {

            //TODO Entferne Überprüfung, da redundant
            if (String.IsNullOrEmpty(userRoleResource.Role))
                return BadRequest();

            if (!await roleManager.RoleExistsAsync(userRoleResource.Role))
                return NotFound(roleManager.RoleExistsAsync(userRoleResource.Role));

            var user = await userManager.FindByNameAsync(userRoleResource.Name);
            if (user == null)
                return BadRequest();

            var result = await userManager.RemoveFromRoleAsync(user, userRoleResource.Role);
            return Ok(await userManager.GetRolesAsync(user));


        }

        //[Authorize(Policy = "HasAdminRights")]
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await userManager.GetUsersInRoleAsync("Student");
            return users;

        }
        
        
        [AllowAnonymous]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordResource changePasswordResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await userManager.FindByNameAsync(changePasswordResource.Name);
            if (user == null)
                return BadRequest();
            await userManager.ChangePasswordAsync(user, changePasswordResource.OldPassword,
                changePasswordResource.NewPassword);
            return Ok();

        }

        [Authorize(Policy = "HasExternalRights")]
        [HttpGet("{id}")]
        public async Task<IEnumerable<string>> GetRoles(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);
            return roles;
        }
        
        private async Task<object> GenerateJwtToken(string email, User user)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(ClaimTypes.NameIdentifier, user.Id)        
            };

            foreach(var role in userRoles){
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["Jwt:ExpireMinutes"]));

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
