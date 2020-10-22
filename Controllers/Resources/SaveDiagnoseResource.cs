using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class SaveDiagnoseResource
    {
         public SaveDiagnoseResource()
        {
            Icds = new Collection<KeyValuePairResource>();
            Synonyms = new Collection<KeyValuePairResource>();
            Diagnostics = new Collection<KeyValuePairResource>();
            Checklists = new Collection<ChecklistResource>();
            Prognoses = new Collection<KeyValuePairResource>();
            Therapies = new Collection<KeyValuePairResource>();
            Symptoms = new Collection<int>();
            Differentialdiagnoses = new Collection<int>();
            Tags = new Collection<int>();

        }
        
        public string UserId { get; set; }
        public Modification Modification { get; set; }
        public int DiagnoseId { get; set; }
        public string TechnicalTerm { get; set; }
        public string Definition { get; set; }
        public string Reason { get; set; } 
        public string AgeTime { get; set; }
        public string Season { get; set; }
        public string Prevalence { get; set; }
        public string Inheritance { get; set; }
        public string FileName { get; set; }
        
        public ICollection<KeyValuePairResource> Icds { get; set; }
        public ICollection<KeyValuePairResource> Synonyms { get; set; }
        public ICollection<KeyValuePairResource> Diagnostics { get; set; }
        public ICollection<ChecklistResource> Checklists { get; set; }
        public ICollection<KeyValuePairResource> Prognoses { get; set; }
        public ICollection<KeyValuePairResource> Therapies { get; set; }
        public ICollection<int> Symptoms { get; set; }
        public ICollection<int> Differentialdiagnoses { get; set; }
        public ICollection<int> Tags { get; set; }
        
        
    }
}