using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class DiagnoseResource
    {
         public DiagnoseResource()
        {
            Checklists = new Collection<ChecklistResource>();
            Prognoses = new Collection<KeyValuePairResource>();
            Symptoms = new Collection<DiagnoseSymptomResource>();
            Therapies = new Collection<KeyValuePairResource>();
            Differentialdiagnoses = new Collection<DifferentialdiagnoseResource>();
            Tags = new Collection<DSTagResource>();
            Icds = new Collection<KeyValuePairResource>();
            Synonyms = new Collection<KeyValuePairResource>();
            Diagnostics = new Collection<KeyValuePairResource>();
        }


        public int DiagnoseId { get; set; }
        public string TechnicalTerm { get; set; }
        public string Definition { get; set; }
        public string Reason { get; set; } 
        public string AgeTime { get; set; }
        public string Season { get; set; }
        public string Prevalence { get; set; }
        public string Inheritance { get; set; }
        
        public ImageResource Image { get; set; }
        public ICollection<DifferentialdiagnoseResource> Differentialdiagnoses { get; set; }
        public ICollection<KeyValuePairResource> Therapies { get; set; }
        public ICollection<ChecklistResource> Checklists { get; set; }
        public ICollection<KeyValuePairResource> Prognoses { get; set; }
        public ICollection<DiagnoseSymptomResource> Symptoms { get; set; }      
        public ICollection<DSTagResource> Tags { get; set; }
        public ICollection<KeyValuePairResource> Icds { get; set; }
        public ICollection<KeyValuePairResource> Synonyms { get; set; }
        public ICollection<KeyValuePairResource> Diagnostics { get; set; }
        
        
    }
}