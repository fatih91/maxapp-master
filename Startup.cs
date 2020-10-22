using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Threading;
using AutoMapper;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using maxapp.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using NSwag.AspNetCore;

namespace maxapp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    //Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    
                    //Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Issuer"],
                    
                    //Validate the token expiry
                    ValidateLifetime = true,
                    
                    // Remove delay of token when expire
                    ClockSkew = TimeSpan.Zero, 
                    
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasAdminRights", policy => policy.RequireRole("Admin"));
                options.AddPolicy("HasExternalRights", policy => policy.RequireRole("Extern"));
            });
            
            services.Configure<ImageSettings>(Configuration.GetSection("ImageSettings"));
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ISymptomRepository, SymptomRepository>();
            services.AddScoped<IDiagnoseRepository, DiagnoseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleValidator<IdentityRole>, RoleValidator<IdentityRole>>();
            services.AddScoped<RoleManager<IdentityRole>, RoleManager<IdentityRole>>();
            services.AddScoped<SignInManager<User>, SignInManager<User>>();
            services.AddScoped<UserManager<User>, UserManager<User>>();
            services.AddHttpContextAccessor();

            services.AddAutoMapper();

          //  services.AddIdentity<User, IdentityRole>(options =>
            //    {
          //          options.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Sub;
          //      })
          //    .AddEntityFrameworkStores<MaxPanelContext>()
          //     .AddDefaultTokenProviders();
            
            IdentityBuilder builder = services.AddIdentityCore<User>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Sub;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<MaxPanelContext>()
                .AddDefaultTokenProviders();
        
            services
                .AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));    
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

        
            
            var connection = Configuration["DbContextSettings:ConnectionString"];
            WaitForDBInit(connection);
            services.AddDbContext<MaxPanelContext>(opts => opts.UseMySql(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MaxPanelContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerReDoc(typeof(Startup).GetTypeInfo().Assembly, settings =>
                {
                    //settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
                    settings.GeneratorSettings.DefaultUrlTemplate = "{controller}/{action}/{id?}";
                    settings.PostProcess = document => { document.Info.Title = "Max App"; };
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            
            context.Database.Migrate();
         //   app.UseHttpsRedirection(); // Bei Dockerbuild auskommentieren ToDo: Docker https fähig machen
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
        
        private static void WaitForDBInit(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            int retries = 1;
            while (retries < 7)
            {
                try
                {
                    Console.WriteLine("Connecting to db. Trial: {0}", retries);
                    connection.Open();
                    connection.Close();
                    break;
                }
                catch (MySqlException)
                {
                    Thread.Sleep((int) Math.Pow(2, retries) * 1000);
                    retries++;
                }
            }
        }
    }
  
}
