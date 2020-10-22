using maxapp.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace maxapp.Persistence
{
    public class MaxPanelContext : IdentityDbContext<User>
    {
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
  

        
        public MaxPanelContext(DbContextOptions<MaxPanelContext> options) : base(options){
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Ignore(u => u.PhoneNumber)
                .Ignore(u => u.PhoneNumberConfirmed)
                .Ignore(u => u.EmailConfirmed)
                .Ignore(u => u.TwoFactorEnabled);

        //    modelBuilder.Ignore<IdentityUserClaim<string>>();
        //    modelBuilder.Ignore<IdentityUserLogin<string>>();
        //    modelBuilder.Ignore<IdentityRoleClaim<string>>();
        //    modelBuilder.Ignore<IdentityUserToken<string>>();


            modelBuilder.Entity<DiagnoseImage>().HasKey(di => 
                new {di.DiagnoseId, di.FileName});
            
            modelBuilder.Entity<DiagnoseSymptom>().HasKey(ds =>
                new {ds.DiagnoseId, ds.SymptomId});

            modelBuilder.Entity<DiagnoseTagAssignment>().HasKey(dta => 
                new {dta.DiagnoseId, dta.TagId});
            
            modelBuilder.Entity<SymptomTagAssignment>().HasKey(sta => 
                new {sta.SymptomId, sta.TagId});
                
            modelBuilder.Entity<Differentialdiagnose>().HasKey(df => 
                new { df.DiagnoseId, df.DifferentialDiagnoseId});
            
            modelBuilder.Entity<Differentialdiagnose>()
                .HasOne(df => df.Diagnose)
                .WithMany(d => d.Differentialdiagnoses)
                .HasForeignKey(df => df.DiagnoseId);

            modelBuilder.Entity<Subcategory>().HasKey(sc =>
                new {sc.CategoryId, sc.SubcategoryId});
            
            modelBuilder.Entity<Subcategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(sc => sc.CategoryId);
            
            modelBuilder.Entity<UserDiagnose>().HasKey(ud =>
                new {ud.DiagnoseId, ud.UserId, ud.LastUpdate});

            modelBuilder.Entity<UserSymptom>().HasKey(us =>
                new {us.SymptomId, us.UserId, us.LastUpdate});

        }
        
    }
}
