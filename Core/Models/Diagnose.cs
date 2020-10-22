using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("Diagnoses")]
    public class Diagnose
    {
        public Diagnose()
        {
            Diagnostics = new Collection<Diagnostic>();
            Synonyms = new Collection<DiagnoseSynonym>();
            Icds = new Collection<Icd>();
            Checklists = new Collection<Checklist>();
            Prognoses = new Collection<Prognose>();
            Therapies = new Collection<Therapy>();
            Symptoms = new Collection<DiagnoseSymptom>();
            Differentialdiagnoses = new Collection<Differentialdiagnose>();
            Tags = new Collection<DiagnoseTagAssignment>();
            Users = new Collection<UserDiagnose>();
        }

        public int DiagnoseId { get; set; }

        [Required]
        [StringLength(255)]
        public string TechnicalTerm { get; set; }

        [StringLength(255)]
        public string Definition { get; set; }
        
        [StringLength(255)]
        public string Reason { get; set; } 
 
        [StringLength(255)]
        public string AgeTime { get; set; }
        
        [StringLength(255)]
        public string Season { get; set; }
        
        [StringLength(255)]
        public string Prevalence { get; set; }
        
        [StringLength(255)] 
        public string Inheritance { get; set; }

        public DiagnoseImage Image { get; set; }

        public ICollection<Diagnostic> Diagnostics { get; set; }

        public ICollection<DiagnoseSynonym> Synonyms { get; set; }

        public ICollection<Icd> Icds { get; set; }

        public ICollection<DiagnoseTagAssignment> Tags { get; set; }
        
        public ICollection<Checklist> Checklists { get; set; }
        
        public ICollection<Prognose> Prognoses { get; set; }

        public ICollection<Therapy> Therapies { get; set; }
        
        public ICollection<DiagnoseSymptom> Symptoms { get; set; }

        public ICollection<Differentialdiagnose> Differentialdiagnoses { get; set; }
        
        public ICollection<UserDiagnose> Users { get; set; }

    }
}