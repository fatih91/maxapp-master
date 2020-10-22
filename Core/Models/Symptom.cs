using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("Symptoms")]
    public class Symptom
    {

        public Symptom()
        {
            Synonyms = new Collection<SymptomSynonym>();
            Diagnoses = new Collection<DiagnoseSymptom>();
            Images = new Collection<Image>();
            Tags = new Collection<SymptomTagAssignment>();
            Users = new Collection<UserSymptom>();
        }
        
        public int SymptomId { get; set; }

        [Required]
        [StringLength(255)]
        public string TechnicalTerm { get; set; }

        [StringLength(255)]
        public string Definition { get; set; }

        public ICollection<SymptomSynonym> Synonyms{ get; set; } 
        
        public ICollection<SymptomTagAssignment> Tags { get; set; }
        
        public ICollection<DiagnoseSymptom> Diagnoses { get; set; }

        public ICollection<Image> Images { get; set; }

        public int? CategoryId { get; set; }
        
        public Category Category { get; set; }

        public ICollection<UserSymptom> Users { get; set; }
    }
}