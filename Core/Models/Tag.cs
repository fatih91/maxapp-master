using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("Tags")]
    public class Tag
    {
        public Tag()
        {
            Symptoms = new Collection<SymptomTagAssignment>();
            Diagnoses = new Collection<DiagnoseTagAssignment>();
        }
        
        public int TagId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<SymptomTagAssignment> Symptoms { get; set; }

        public ICollection<DiagnoseTagAssignment> Diagnoses { get; set; }
    }
}