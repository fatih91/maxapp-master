using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using AutoMapper;
using maxapp.Controllers.Resources;
using maxapp.Core.Models;
using maxapp.Persistence;
using Microsoft.CodeAnalysis;

namespace maxapp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource

            CreateMap<User, UserDiagnoseResource>()
                .ForMember(ur => ur.Diagnoses, opt => opt.Ignore())

                /*.ForMember(ur => ur.Diagnoses, opt => opt.MapFrom(u => u.Diagnoses.Select(ud =>
                    new UserContentResource()
                    {
                        Id = ud.Diagnose.DiagnoseId,
                        TechnicalTerm = ud.Diagnose.TechnicalTerm,
                        Synonym = ud.Diagnose.Synonyms.Select(s => s.Name).ToList().FirstOrDefault(),
                        Image = new DiagnoseImage{
                            FileName = ud.Diagnose?.Image.FileName
                        }   
                    }
                )))*/
                .AfterMap((u, ur) =>
                {
                    var diagnoses = u.Diagnoses.GroupBy(d => d.Diagnose.DiagnoseId).Select(d => d.First());
                    foreach (var diagnose in diagnoses)
                    {
                        var diagnoseResource = new UserContentResource
                        {
                            Id = diagnose.Diagnose.DiagnoseId,
                            TechnicalTerm = diagnose.Diagnose.TechnicalTerm,
                            Synonym = diagnose.Diagnose.Synonyms.Select(s => s.Name).ToList().FirstOrDefault(),
                        };
                        if (diagnose.Diagnose.Image != null)
                            diagnoseResource.FileName = diagnose.Diagnose.Image.FileName;
                        ur.Diagnoses.Add(diagnoseResource);
                    }

                });


            //todo
            CreateMap<User, UserSymptomResource>()
                .ForMember(ur => ur.Symptoms, opt => opt.Ignore())
                .AfterMap((u, ur) =>
                {

                    var symptoms = u.Symptoms.GroupBy(s => s.Symptom.SymptomId).Select(s => s.First());
                    foreach (var symptom in symptoms)
                    {
                        var symptomResource = new UserContentResource
                        {
                            Id = symptom.Symptom.SymptomId,
                            TechnicalTerm = symptom.Symptom.TechnicalTerm,
                            Synonym = symptom.Symptom.Synonyms.Select(s => s.Name).ToList().FirstOrDefault(),
                        };

                        if (symptom.Symptom.Images.FirstOrDefault() != null)
                        {
                            symptomResource.FileName = symptom.Symptom.Images.FirstOrDefault().FileName;
                        }
                     
                        ur.Symptoms.Add(symptomResource);

                    }

                });
        



        CreateMap<Category, MapCategoryResource>()
                .ForMember(mcr => mcr.Subcategories, opt => opt.MapFrom(c => c.Subcategories.Select(sc => 
                    new SubcategoryResource
                    {
                        Name = sc.SubCategory.Name,
                        SubcategoryId = sc.SubCategory.CategoryId
                    }
                )));

            CreateMap<Tag, DSTagResource>();

            CreateMap<Image, SaveImageResource>();

            CreateMap<Subcategory, ParentCategoryResource>()
                .ForMember(pcr => pcr.ParentId, opt => opt.MapFrom(sc => sc.Category.CategoryId))
                .ForMember(pcr => pcr.Name, opt => opt.MapFrom(sc => sc.Category.Name));

            CreateMap<FilterResource, Filter>()
                .ForMember(f => f.SearchTerm, opt => opt.MapFrom(fr => fr.Q));

            CreateMap<Image, ImageResource>();

            CreateMap<Diagnose, DiagnoseResource>()
                .ForMember(dr => dr.Checklists, opt => opt.MapFrom(d => d.Checklists.Select(c =>
                    new ChecklistResource
                    {
                        ChecklistId = c.Id,
                        Checkup = c.Checkup,
                        Reason = c.Reason
                    }))) // Checklists
                .ForMember(dr => dr.Prognoses, opt => opt.MapFrom(d => d.Prognoses.Select(p =>
                    new KeyValuePairResource
                    {
                        Id = p.Id,
                        Value = p.Description
                    }))) // Prognoses
                .ForMember(dr => dr.Synonyms, opt => opt.MapFrom(d => d.Synonyms.Select(p =>
                    new KeyValuePairResource
                    {
                        Id = p.DiagnoseSynonymId,
                        Value = p.Name
                    }))) // Synonyms
                .ForMember(dr => dr.Diagnostics, opt => opt.MapFrom(d => d.Diagnostics.Select(p =>
                    new KeyValuePairResource
                    {
                        Id = p.DiagnosticId,
                        Value = p.Name
                    }))) // Diagnostics
                .ForMember(dr => dr.Therapies, opt => opt.MapFrom(d => d.Therapies.Select(t =>
                    new KeyValuePairResource
                    {
                        Id = t.Id,
                        Value = t.Description
                    }))) // Therapies
                .ForMember(dr => dr.Icds, opt => opt.MapFrom(d => d.Icds.Select(i =>
                    new KeyValuePairResource
                    {
                        Id = i.IcdId,
                        Value = i.Name
                    }))) // Icds
                .ForMember(dr => dr.Symptoms, opt => opt.MapFrom(d => d.Symptoms.Select(ds =>
                    new DiagnoseSymptomResource
                    {
                        Id = ds.Symptom.SymptomId,
                        TechnicalTerm = ds.Symptom.TechnicalTerm,
                        Synonym = ds.Symptom.Synonyms.Select(ss => ss.Name
                        ).FirstOrDefault(),
                        Definition = ds.Symptom.Definition
                    }))) //Diagnose-Symptom
                .ForMember(dr => dr.Differentialdiagnoses, opt => opt.MapFrom(d => d.Differentialdiagnoses.Select(df =>
                    new DifferentialdiagnoseResource
                    {
                        DiagnoseId = df.DifferentialDiagnose.DiagnoseId,
                        TechnicalTerm = df.DifferentialDiagnose.TechnicalTerm,
                        Prevalence = df.DifferentialDiagnose.Prevalence,
                        Icd = df.DifferentialDiagnose.Icds.Select(dfs => dfs.Name).FirstOrDefault(),
                        Synonym = df.DifferentialDiagnose.Synonyms.Select(dfs => dfs.Name).FirstOrDefault(),
                        
                    }))) //Differentialdiagnoses
                .ForMember(dr => dr.Tags, opt => opt.MapFrom(d => d.Tags.Select(t =>
                    new DSTagResource
                    {
                        TagId = t.Tag.TagId,
                        Name = t.Tag.Name
                    }))) // Tags
                .ForMember(dr => dr.Image, opt => opt.MapFrom(d => 
                    new ImageResource
                    {
                        FileName = d.Image.Image.FileName,
                        Age = d.Image.Image.Age,
                        Gender = d.Image.Image.Gender,
                        ImageDescription = d.Image.Image.ImageDescription
                    })); //Images
                
                //ToDo: Suchbild Überarbeiten
                /*
                .AfterMap((d, dr) =>
                {
                    var images = d.Symptoms.SelectMany(ds => ds.Symptom.Images);
                    
                    foreach (var i in images)
                    {

                        dr.Images.Add(new ImageResource
                        {
                            FileName = i.FileName,
                            ImageDescription = i.ImageDescription,
                            Gender = i.Gender,
                            Age = i.Age

                        });
                    }

                    if (images.Any())
                    {
                        dr.ImageDiagnose = new ImageResource
                        {
                            Age = images.FirstOrDefault().Age,
                            FileName = images.FirstOrDefault().FileName,
                            Gender = images.FirstOrDefault().Gender,
                            ImageDescription = images.FirstOrDefault().ImageDescription,
                            ImageId = images.FirstOrDefault().ImageId
                        };       
                    }
                });
                */

            CreateMap<Symptom, SymptomResource>()
                .ForMember(sr => sr.Images, opt => opt.MapFrom((s => s.Images.Select(i =>
                    new ImageResource
                    {
                        FileName = i.FileName,
                        ImageDescription = i.ImageDescription,
                        Age = i.Age,
                        Gender = i.Gender,
                    }   
                    )))) // Images
                .ForMember(sr => sr.Synonyms, opt => opt.MapFrom(s => s.Synonyms.Select(syn => 
                    new KeyValuePairResource
                    {
                        Id = syn.SymptomSynonymId,
                        Value = syn.Name
                    }))) // Synonyms
                
                .ForMember(sr => sr.Diagnoses, opt => opt.MapFrom(s => s.Diagnoses.Select(ds => 
                    new SymptomDiagnoseResource
                    {
                        DiagnoseId = ds.Diagnose.DiagnoseId,
                        TechnicalTerm = ds.Diagnose.TechnicalTerm,
                        Icds = ds.Diagnose.Icds.Select(dsy => 
                            new KeyValuePairResource
                            {
                                Id = dsy.IcdId, 
                                Value = dsy.Name
                            }).ToList(),
                        Prevalence = ds.Diagnose.Prevalence,
                        Synonyms = ds.Diagnose.Synonyms.Select(dsy => 
                            new KeyValuePairResource
                            {
                                Id = dsy.DiagnoseSynonymId, 
                                Value = dsy.Name
                            }).ToList(),
                        
                    }))) // Diagnoses
                .ForMember(sr => sr.Tags, opt => opt.MapFrom(s => s.Tags.Select(t => 
                    new DSTagResource
                    {
                        TagId = t.Tag.TagId,
                        Name = t.Tag.Name
                    }))); // Tags


            CreateMap<Category, CategoryResource>()
                .ForMember(cr => cr.Subcategories, opt => opt.MapFrom(c =>
                    c.Subcategories.Select(sc =>
                        new SubcategoryResource
                        {
                            Name = sc.SubCategory.Name,
                            SubcategoryId = sc.SubcategoryId

                        })))
                .ForMember(cr => cr.Symptoms, opt => opt.MapFrom(c => c.Symptoms.Select(symptom =>
                
                   new CategorySymptomResource
                    {
                        SymptomId = symptom.SymptomId,
                        TechnicalTerm = symptom.TechnicalTerm,
                        Synonym = symptom.Synonyms.Select(s => s.Name).ToList().FirstOrDefault(),
                        FileName = symptom.Images.Select(i => i.FileName).ToList().FirstOrDefault()
                    }
                )));
                
            
            CreateMap<Subcategory, CategoryResource>()
                  .ForMember(cr => cr.CategoryId, opt => opt.MapFrom(sc => sc.CategoryId))
                  .ForMember(cr => cr.Name, opt => opt.MapFrom(sc => sc.Category.Name))
                  .ForMember(cr => cr.Subcategories, opt => opt.Ignore())
                  .ForMember(cr => cr.Symptoms, opt => opt.MapFrom(sc => 
                    sc.Category.Symptoms.Select(s => 
                    new CategorySymptomResource
                {
                    SymptomId = s.SymptomId,
                    TechnicalTerm = s.TechnicalTerm,
                    Synonym= s.Synonyms.Select(sy => sy.Name).ToList().FirstOrDefault()
               
                })));

            
            CreateMap<Tag, TagResource>()
                .ForMember(tr => tr.Diagnoses, opt => opt.MapFrom(ta => ta.Diagnoses
                    .Where(d => d.Diagnose != null) 
                    .Select(d =>
                        new TagDiagnoseResource
                        {
                            DiagnoseId = d.Diagnose.DiagnoseId,
                            Synonyms = d.Diagnose.Synonyms.Select(ds =>
                                    new KeyValuePairResource
                                    {
                                        Id = ds.DiagnoseSynonymId, 
                                        Value = ds.Name
                                    }).ToList()
                                ,
                            TechnicalTerm = d.Diagnose.TechnicalTerm,
                            Icds = d.Diagnose.Icds.Select(ds =>
                                new KeyValuePairResource
                                {
                                    Id = ds.DiagnoseId, 
                                    Value = ds.Name
                                }).ToList()
                        })))
                .ForMember(tr => tr.Symptoms, opt => opt.MapFrom(ta => ta.Symptoms
                    .Where(s => s.Symptom != null)
                    .Select(s => 
                        new TagSymptomResource
                        {
                            SymptomId   = s.Symptom.SymptomId,
                            Synonyms = s.Symptom.Synonyms.Select(ss => 
                                new KeyValuePairResource
                                {
                                    Id = ss.SymptomSynonymId, 
                                    Value = ss.Name
                                }).ToList(),
                            TechnicalTerm = s.Symptom.TechnicalTerm
                        })));
                
            // API Resource to Domain
            CreateMap<DSTagResource, Tag>()
                .ForMember(t => t.TagId, opt => opt.Ignore());

            CreateMap<SaveSymptomResource, Symptom>()
                .ForMember(s => s.SymptomId, opt => opt.Ignore())
                .ForMember(s => s.Diagnoses, opt => opt.Ignore())
                .ForMember(s => s.Synonyms, opt => opt.Ignore())
                .ForMember(d => d.Users, opt => opt.Ignore())
                .ForMember(s => s.CategoryId, opt => opt.Ignore())
                .ForMember(d => d.Tags, opt => opt.Ignore())
                .AfterMap((sr, s) =>
                {
                    if (sr.CategoryId == 0)
                    {
                        s.CategoryId = 1;
                    }  else
                    {
                        s.CategoryId = sr.CategoryId;
                    }
                    
                    //Remove Diagnoses
                    var removedDiagnose = s.Diagnoses
                        .Where(ds => !sr.Diagnoses.Contains(ds.DiagnoseId)).ToList();

                    foreach (var sd in removedDiagnose)
                        s.Diagnoses.Remove(sd);

                    //Add Diagnoses
                    var addedDiagnoses = sr.Diagnoses
                        .Where(id => s.Diagnoses.All(ds => ds.DiagnoseId != id))
                        .Select(id => new DiagnoseSymptom {DiagnoseId = id}).ToList();

                    foreach (var sd in addedDiagnoses.ToList())
                        s.Diagnoses.Add(sd);
                    
                    //Remove Synonyms
                    var removedSynoyms = s.Synonyms
                        .Where(ss => sr.Synonyms.All(syn => syn.Id != ss.SymptomSynonymId)).ToList();
                    foreach (var rs in removedSynoyms)
                    {
                        s.Synonyms.Remove(rs);
                    }
                    
                    //Add new Synonyms
                    var addedSynonyms = sr.Synonyms
                        .Where(ss => s.Synonyms.All(syn => syn.SymptomSynonymId != ss.Id))
                        .Select(ss => new SymptomSynonym {SymptomSynonymId= ss.Id, Name = ss.Value}).ToList();
                    foreach (var ads in addedSynonyms)
                    {
                        s.Synonyms.Add(ads);
                    }
                    
                    //If Synonyms content changed
                    foreach (var cs in sr.Synonyms.ToList())
                    {
                        var changedSynonym = s.Synonyms.FirstOrDefault(ss => ss.SymptomSynonymId == cs.Id && cs.Id != 0);
                        if (changedSynonym != null)
                        {

                            changedSynonym.Name = cs.Value;
                        }
                    }
                    
                    // User add new Symptom or modificate Symptom
                    var user = new UserSymptom
                    {
                        UserId = sr.UserId,
                        LastUpdate = DateTime.Now
                    };
                    
                    switch (sr.Modification)
                    {
                        case Modification.Created:
                        {
                            user.Modification = Modification.Created;
                            break;
                        }
                        case Modification.Edited:
                        {
                            user.Modification = Modification.Edited;
                            break;
                        }
                    }
                    
                    s.Users.Add(user);
                    
                    
                    //Remove Tags
                    var removedTags = s.Tags
                        .Where(ta => !sr.Tags.Contains(ta.TagId)).ToList();
                    
                    foreach (var t in removedTags)
                        s.Tags.Remove(t);
                    
                    //Add Tags
                    var addedTags = sr.Tags
                        .Where(id => s.Tags.All(ta => ta.TagId != id))
                        .Select(id => new SymptomTagAssignment {TagId = id}).ToList();

                    foreach (var t in addedTags.ToList())
                        s.Tags.Add(t);
                });
            

            CreateMap<SaveDiagnoseResource, Diagnose>()
                .ForMember(d => d.DiagnoseId, opt => opt.Ignore())
                .ForMember(d => d.Users, opt => opt.Ignore())
                .ForMember(d => d.Checklists, opt => opt.Ignore())
                .ForMember(d => d.Prognoses, opt => opt.Ignore())
                .ForMember(d => d.Therapies, opt => opt.Ignore())
                .ForMember(d => d.Icds, opt => opt.Ignore())
                .ForMember(d => d.Diagnostics, opt => opt.Ignore())
                .ForMember(d => d.Synonyms, opt => opt.Ignore())
                .ForMember(d => d.Symptoms, opt => opt.Ignore())
                .ForMember(d => d.Image, opt => opt.Ignore())
                .ForMember(d => d.Differentialdiagnoses, opt => opt.Ignore())
                .ForMember(d => d.Tags, opt => opt.Ignore())
                .AfterMap((dr, d) =>
                {
                    //Remove Checklists
                    var removedChecklists = d.Checklists
                        .Where(c => dr.Checklists.All(cr => cr.ChecklistId != c.Id)).ToList();    
                    
                    foreach (var c in removedChecklists)
                        d.Checklists.Remove(c);
                
                    //Add new Checklists
                    var addedChecklists = dr.Checklists
                        .Where(cr => d.Checklists.All(c => c.Id != cr.ChecklistId))
                        .Select(cr => 
                            new Checklist
                            {
                                Id = cr.ChecklistId, 
                                Checkup = cr.Checkup, 
                                Reason = cr.Reason
                            }).ToList();
                    
                    foreach (var c in addedChecklists.ToList())
                        d.Checklists.Add(c);
                                        
                   //If Checklist content changed
                     foreach (var cr in dr.Checklists.ToList()){
                       var changedChecklist = d.Checklists.FirstOrDefault(c => c.Id == cr.ChecklistId && cr.ChecklistId != 0);
                       if (changedChecklist != null)
                       {
                           changedChecklist.Checkup = cr.Checkup;
                           changedChecklist.Reason = cr.Reason;
                       }
                     }
                    
                    //Remove Icds
                    var removedIcds = d.Icds
                        .Where(i => dr.Icds.All(ir => ir.Id != i.IcdId)).ToList();

                    foreach (var i in removedIcds)
                        d.Icds.Remove(i);
                    
                    //Add new Icds
                    var addedIcds = dr.Icds
                        .Where(ir => d.Icds.All(i => i.IcdId != ir.Id))
                        .Select(ir =>
                            new Icd
                            {
                                IcdId  = ir.Id,
                                Name = ir.Value
                            })
                        .ToList();
                    
                    foreach (var i in addedIcds.ToList())
                        d.Icds.Add(i);
                    
                    //If Icds content changed
                    foreach (var ir in dr.Icds.ToList())
                    {
                        var changedIcd = d.Icds.FirstOrDefault(i => i.IcdId == ir.Id && ir.Id != 0);
                        if (changedIcd != null)
                        {
                            changedIcd.Name = ir.Value;
                        }
                    }
                    
                    
                    //Remove Diagnostics
                    var removedDiagnostics = d.Diagnostics
                        .Where(dg => dr.Diagnostics.All(dgr => dgr.Id != dg.DiagnosticId)).ToList();

                    foreach (var di in removedDiagnostics)
                        d.Diagnostics.Remove(di);
                        
                    //Add new Diagnostics
                    var addedDiagnostics = dr.Diagnostics
                        .Where(dir => d.Diagnostics.All(di => di.DiagnosticId != dir.Id))
                        .Select(dir => 
                            new maxapp.Core.Models.Diagnostic
                            {
                                DiagnosticId = dir.Id,
                                Name = dir.Value
                            })
                        .ToList();

                    foreach (var adi in addedDiagnostics.ToList())
                        d.Diagnostics.Add(adi);
                    
                    
                    //If Diagnostics content changed
                    foreach (var di in dr.Diagnostics.ToList())
                    {
                        var changedDiagnostic = d.Diagnostics.FirstOrDefault(dir => dir.DiagnosticId == di.Id && di.Id != 0);
                        if (changedDiagnostic != null)
                        {
                            changedDiagnostic.Name = di.Value;
                        }
                    }
                    
                    //Remove Synonyms
                    var removedSynoyms = d.Synonyms
                        .Where(ds => dr.Synonyms.All(syn => syn.Id != ds.DiagnoseSynonymId)).ToList();
                    foreach (var s in removedSynoyms)
                    {
                        d.Synonyms.Remove(s);
                    }
                    
                    //Add new Synonyms
                    var addedSynonyms = dr.Synonyms
                        .Where(ds => d.Synonyms.All(syn => syn.DiagnoseSynonymId != ds.Id))
                        .Select(ds => new DiagnoseSynonym {DiagnoseSynonymId = ds.Id, Name = ds.Value}).ToList();
                    foreach (var s in addedSynonyms)
                    {
                        d.Synonyms.Add(s);
                    }
                    
                    //If Synonyms content changed
                    foreach (var s in dr.Synonyms.ToList())
                    {
                        var changedSynonym = d.Synonyms.FirstOrDefault(ds => ds.DiagnoseSynonymId == s.Id && s.Id != 0);
                        if (changedSynonym != null)
                        {

                            changedSynonym.Name = s.Value;
                        }
                    }
                    
                    //Remove Therapies
                    var removedTherapies = d.Therapies
                        .Where(t => dr.Therapies.All(tr => tr.Id != t.Id)).ToList();    
                    
                    foreach (var t in removedTherapies)
                        d.Therapies.Remove(t);
                
                    //Add new Therapy
                    var addedTherapies = dr.Therapies
                        .Where(tr => d.Therapies.All(t => t.Id != tr.Id))
                        .Select(tr => new Therapy
                        {
                            Id = tr.Id, 
                            Description = tr.Value
                        }).ToList();
                    
                    foreach (var t in addedTherapies.ToList())
                        d.Therapies.Add(t);
                                        
                    //If Therapies content changed
                    
                    foreach (var tr in dr.Therapies.ToList()){
                        var changedTherapy = d.Therapies.FirstOrDefault(t => t.Id == tr.Id && tr.Id != 0);
                        if (changedTherapy != null)
                        {
                            changedTherapy.Description = tr.Value;
                        }
                    }
                    
                    //Remove Prognose
                    var removedPrognoses = d.Prognoses
                        .Where(p => dr.Prognoses.All(pr => pr.Id != p.Id)).ToList();
                    
                    foreach (var p in removedPrognoses)
                        d.Prognoses.Remove(p);
                    
                    //Add Prognose
                    var addedPrognoses = dr.Prognoses
                        .Where(pr => d.Prognoses.All(p => p.Id != pr.Id))
                        .Select(pr => new Prognose
                        {
                            Id = pr.Id, 
                            Description = pr.Value
                        }).ToList();
                    
                    foreach (var p in addedPrognoses.ToList())
                        d.Prognoses.Add(p);
                    
                    //If Prognose content changed
                    foreach (var pr in dr.Prognoses.ToList())
                    {
                        var changedPrognose = d.Prognoses.FirstOrDefault(p => p.Id == pr.Id && pr.Id != 0);
                        if (changedPrognose != null)
                        {
                            changedPrognose.Description = pr.Value;
                        }
                    }
                    
                    /*
                     * Referenzierungen auf bestehende
                     * Symptome, Differentialdiagnosen, User und Tags
                     */
                    
                    //Remove Symptom
                    var removedSymptoms = d.Symptoms
                        .Where(ds => !dr.Symptoms.Contains(ds.SymptomId)).ToList();
                    
                    foreach (var s in removedSymptoms)
                        d.Symptoms.Remove(s);

                    //Add Symptom
                    var addedSymptoms = dr.Symptoms
                        .Where(id => d.Symptoms.All(ds => ds.SymptomId != id))
                        .Select(id => new DiagnoseSymptom {SymptomId = id}).ToList();

                    foreach (var s in addedSymptoms.ToList())
                        d.Symptoms.Add(s);
                    
                    
                    // User add new Diagnose or modificate Diagnose
                    var user = new UserDiagnose
                    {
                        UserId = dr.UserId,
                        LastUpdate = DateTime.Now
                    };
                    
                    switch (dr.Modification)
                    {
                        case Modification.Created:
                        {
                            user.Modification = Modification.Created;
                            break;
                        }
                        case Modification.Edited:
                        {
                            user.Modification = Modification.Edited;
                            break;
                        }
                    }
                    
                    d.Users.Add(user);
                    
                    
                    //Add Image or remove image
                    if (!String.IsNullOrEmpty(dr.FileName))
                    {
                        d.Image = new DiagnoseImage {FileName = dr.FileName};
                    }
                    else
                    {
                        d.Image = null;
                    }
                    
                    
                    
                    
                    //Remove Differentialdiagnoses
                    var removedDifferentialdiagnoses = d.Differentialdiagnoses
                        .Where(df => !dr.Differentialdiagnoses.Contains(df.DifferentialDiagnoseId)).ToList();

                    foreach (var df in removedDifferentialdiagnoses)
                        d.Differentialdiagnoses.Remove(df);

                    //Add Differentialdiagnoses
                    var addDifferentialdiagnoses = dr.Differentialdiagnoses
                        .Where(id =>
                            d.Differentialdiagnoses.All(df =>
                                df.DifferentialDiagnoseId != id &&
                                id != d.DiagnoseId)) //Prüfe nach, ob die Diagnose nicht zu sich selbst referenziert 
                        .Select(id => new Differentialdiagnose {DifferentialDiagnoseId = id}).ToList();


                    foreach (var df in addDifferentialdiagnoses.ToList())
                        d.Differentialdiagnoses.Add(df);
                    
                    
                    
                    //Remove Tags
                    var removedTags = d.Tags
                        .Where(ta => !dr.Tags.Contains(ta.TagId)).ToList();
                    
                    foreach (var t in removedTags)
                        d.Tags.Remove(t);
                    
                    //Add Tags
                    var addedTags = dr.Tags
                        .Where(id => d.Tags.All(ta => ta.TagId != id))
                        .Select(id => new DiagnoseTagAssignment {TagId = id}).ToList();

                    foreach (var t in addedTags.ToList())
                        d.Tags.Add(t);
                });

            CreateMap<SaveCategoryResource, Category>()
                .ForMember(c => c.CategoryId, opt => opt.Ignore())
                .ForMember(c => c.Subcategories, opt => opt.Ignore())
                .ForMember(c => c.Symptoms, opt => opt.Ignore())
                .AfterMap((scr, c) =>
                {
                    /*//Remove Symptoms
                    var removedSymptoms = c.Symptoms
                        .Where(s => !scr.Symptoms.Contains(s.SymptomId)).ToList();

                    foreach (var s in removedSymptoms)
                        c.Symptoms.Remove(s);*/
                });
            
            CreateMap<SaveCategoryResource, Subcategory>()
                .ForMember(sc => sc.CategoryId, opt => opt.MapFrom(cr => cr.ParentId))
                .ForMember(sc => sc.Category, opt => opt.Ignore())
                .ForMember(sc => sc.SubcategoryId, opt => opt.Ignore())
                .ForMember(sc => sc.SubCategory, opt => opt.Ignore())
                .AfterMap((scr, sc) =>
                {
                    if (sc.SubCategory == null)
                        sc.SubCategory = new Category{ Name = scr.Name };                    
                });

            CreateMap<Category, SaveCategoryResource>()
                .ForMember(scr => scr.Name, opt => opt.Ignore())
                .ForMember(scr => scr.ParentId, opt => opt.Ignore())
                .ForMember(scr => scr.Symptoms, opt => opt.Ignore())
                .AfterMap((c, scr) =>
                {
                    c.Name = scr.Name;
                    //Remove Symptoms
                    var removedSymptoms = c.Symptoms.Where(s => !scr.Symptoms.Contains(s.SymptomId)).ToList();

                    foreach (var s in removedSymptoms)
                        c.Symptoms.Remove(s);
                    
                    // Add Symptoms
                    var removeExistingSymptomsFromResource = scr.Symptoms
                        .Where(id => c.Symptoms.Any(s => s.SymptomId == id)).ToList();

                    foreach (var s in removeExistingSymptomsFromResource)
                        scr.Symptoms.Remove(s);
                });

            CreateMap<ICollection<Symptom>, Category>()
                .ForMember(c => c.CategoryId, opt => opt.Ignore())
                .ForMember(c => c.Name, opt => opt.Ignore())
                .ForMember(c => c.Subcategories, opt => opt.Ignore())
                .ForMember(c => c.Symptoms, opt => opt.Ignore())
                .AfterMap((symptoms, c) =>
                {
                    foreach (var s in symptoms)
                        c.Symptoms.Add(s);
                });

            CreateMap<Category, Subcategory>()
                .ForMember(sc => sc.SubCategory, opt => opt.MapFrom(c => c))
                .ForMember(sc => sc.SubcategoryId, opt => opt.MapFrom(c => c.CategoryId))
                .ForMember(sc => sc.CategoryId, opt => opt.Ignore())
                .ForMember(sc => sc.Category, opt => opt.Ignore());

            CreateMap<SaveImageResource, Image>()
                .ForMember(i => i.FileName, opt => opt.Ignore());

        }
    }
}