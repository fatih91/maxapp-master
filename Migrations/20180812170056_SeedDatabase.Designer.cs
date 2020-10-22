﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using maxapp.Persistence;

namespace maxapp.Migrations
{
    [DbContext(typeof(MaxPanelContext))]
    [Migration("20180812170056_SeedDatabase")]
    partial class SeedDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("maxapp.Core.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("maxapp.Core.Models.Checklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Checkup")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("DiagnoseId");

                    b.Property<string>("Reason")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("DiagnoseId");

                    b.ToTable("Checklists");
                });

            modelBuilder.Entity("maxapp.Core.Models.Diagnose", b =>
                {
                    b.Property<int>("DiagnoseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgeTime")
                        .HasMaxLength(255);

                    b.Property<string>("Definition")
                        .HasMaxLength(255);

                    b.Property<string>("Inheritance")
                        .HasMaxLength(255);

                    b.Property<string>("Prevalence")
                        .HasMaxLength(255);

                    b.Property<string>("Reason")
                        .HasMaxLength(255);

                    b.Property<string>("Season")
                        .HasMaxLength(255);

                    b.Property<string>("TechnicalTerm")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("DiagnoseId");

                    b.ToTable("Diagnoses");
                });

            modelBuilder.Entity("maxapp.Core.Models.DiagnoseImage", b =>
                {
                    b.Property<int>("DiagnoseId");

                    b.Property<string>("FileName");

                    b.HasKey("DiagnoseId", "FileName");

                    b.HasIndex("DiagnoseId")
                        .IsUnique();

                    b.HasIndex("FileName");

                    b.ToTable("DiagnoseImage");
                });

            modelBuilder.Entity("maxapp.Core.Models.DiagnoseSymptom", b =>
                {
                    b.Property<int>("DiagnoseId");

                    b.Property<int>("SymptomId");

                    b.HasKey("DiagnoseId", "SymptomId");

                    b.HasIndex("SymptomId");

                    b.ToTable("DiagnoseSymptoms");
                });

            modelBuilder.Entity("maxapp.Core.Models.DiagnoseSynonym", b =>
                {
                    b.Property<int>("DiagnoseSynonymId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DiagnoseId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("DiagnoseSynonymId");

                    b.HasIndex("DiagnoseId");

                    b.ToTable("DiagnoseSynonyms");
                });

            modelBuilder.Entity("maxapp.Core.Models.DiagnoseTagAssignment", b =>
                {
                    b.Property<int>("DiagnoseId");

                    b.Property<int>("TagId");

                    b.HasKey("DiagnoseId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("DiagnoseTagAssignments");
                });

            modelBuilder.Entity("maxapp.Core.Models.Diagnostic", b =>
                {
                    b.Property<int>("DiagnosticId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DiagnoseId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("DiagnosticId");

                    b.HasIndex("DiagnoseId");

                    b.ToTable("Diagnostics");
                });

            modelBuilder.Entity("maxapp.Core.Models.Differentialdiagnose", b =>
                {
                    b.Property<int>("DiagnoseId");

                    b.Property<int>("DifferentialDiagnoseId");

                    b.HasKey("DiagnoseId", "DifferentialDiagnoseId");

                    b.HasIndex("DifferentialDiagnoseId");

                    b.ToTable("Differentialdiagnoses");
                });

            modelBuilder.Entity("maxapp.Core.Models.Icd", b =>
                {
                    b.Property<int>("IcdId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DiagnoseId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("IcdId");

                    b.HasIndex("DiagnoseId");

                    b.ToTable("Icds");
                });

            modelBuilder.Entity("maxapp.Core.Models.Image", b =>
                {
                    b.Property<string>("FileName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255);

                    b.Property<int>("Age");

                    b.Property<int>("Gender");

                    b.Property<string>("ImageDescription")
                        .HasMaxLength(255);

                    b.Property<int>("SymptomId");

                    b.HasKey("FileName");

                    b.HasIndex("SymptomId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("maxapp.Core.Models.Prognose", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("DiagnoseId");

                    b.HasKey("Id");

                    b.HasIndex("DiagnoseId");

                    b.ToTable("Prognoses");
                });

            modelBuilder.Entity("maxapp.Core.Models.Subcategory", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("SubcategoryId");

                    b.HasKey("CategoryId", "SubcategoryId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Subcategories");
                });

            modelBuilder.Entity("maxapp.Core.Models.Symptom", b =>
                {
                    b.Property<int>("SymptomId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Definition")
                        .HasMaxLength(255);

                    b.Property<string>("TechnicalTerm")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("SymptomId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Symptoms");
                });

            modelBuilder.Entity("maxapp.Core.Models.SymptomSynonym", b =>
                {
                    b.Property<int>("SymptomSynonymId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("SymptomId");

                    b.HasKey("SymptomSynonymId");

                    b.HasIndex("SymptomId");

                    b.ToTable("SymptomSynonyms");
                });

            modelBuilder.Entity("maxapp.Core.Models.SymptomTagAssignment", b =>
                {
                    b.Property<int>("SymptomId");

                    b.Property<int>("TagId");

                    b.HasKey("SymptomId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("SymptomTagAssignments");
                });

            modelBuilder.Entity("maxapp.Core.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("maxapp.Core.Models.Therapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("DiagnoseId");

                    b.HasKey("Id");

                    b.HasIndex("DiagnoseId");

                    b.ToTable("Therapies");
                });

            modelBuilder.Entity("maxapp.Core.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("Submitted");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("maxapp.Core.Models.UserDiagnose", b =>
                {
                    b.Property<int>("DiagnoseId");

                    b.Property<string>("UserId");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("Modification");

                    b.HasKey("DiagnoseId", "UserId", "LastUpdate");

                    b.HasIndex("UserId");

                    b.ToTable("UserDiagnose");
                });

            modelBuilder.Entity("maxapp.Core.Models.UserSymptom", b =>
                {
                    b.Property<int>("SymptomId");

                    b.Property<string>("UserId");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("Modification");

                    b.HasKey("SymptomId", "UserId", "LastUpdate");

                    b.HasIndex("UserId");

                    b.ToTable("UserSymptom");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("maxapp.Core.Models.Checklist", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Checklists")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.DiagnoseImage", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithOne("Image")
                        .HasForeignKey("maxapp.Core.Models.DiagnoseImage", "DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("maxapp.Core.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("FileName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.DiagnoseSymptom", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Symptoms")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("maxapp.Core.Models.Symptom", "Symptom")
                        .WithMany("Diagnoses")
                        .HasForeignKey("SymptomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.DiagnoseSynonym", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Synonyms")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.DiagnoseTagAssignment", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Tags")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("maxapp.Core.Models.Tag", "Tag")
                        .WithMany("Diagnoses")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.Diagnostic", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Diagnostics")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.Differentialdiagnose", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Differentialdiagnoses")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("maxapp.Core.Models.Diagnose", "DifferentialDiagnose")
                        .WithMany()
                        .HasForeignKey("DifferentialDiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.Icd", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Icds")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.Image", b =>
                {
                    b.HasOne("maxapp.Core.Models.Symptom", "Symptom")
                        .WithMany("Images")
                        .HasForeignKey("SymptomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.Prognose", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Prognoses")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.Subcategory", b =>
                {
                    b.HasOne("maxapp.Core.Models.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("maxapp.Core.Models.Category", "SubCategory")
                        .WithMany()
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.Symptom", b =>
                {
                    b.HasOne("maxapp.Core.Models.Category", "Category")
                        .WithMany("Symptoms")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("maxapp.Core.Models.SymptomSynonym", b =>
                {
                    b.HasOne("maxapp.Core.Models.Symptom", "Symptom")
                        .WithMany("Synonyms")
                        .HasForeignKey("SymptomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.SymptomTagAssignment", b =>
                {
                    b.HasOne("maxapp.Core.Models.Symptom", "Symptom")
                        .WithMany("Tags")
                        .HasForeignKey("SymptomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("maxapp.Core.Models.Tag", "Tag")
                        .WithMany("Symptoms")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.Therapy", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Therapies")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.UserDiagnose", b =>
                {
                    b.HasOne("maxapp.Core.Models.Diagnose", "Diagnose")
                        .WithMany("Users")
                        .HasForeignKey("DiagnoseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("maxapp.Core.Models.User", "User")
                        .WithMany("Diagnoses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("maxapp.Core.Models.UserSymptom", b =>
                {
                    b.HasOne("maxapp.Core.Models.Symptom", "Symptom")
                        .WithMany("Users")
                        .HasForeignKey("SymptomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("maxapp.Core.Models.User", "User")
                        .WithMany("Symptoms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("maxapp.Core.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("maxapp.Core.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("maxapp.Core.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("maxapp.Core.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}