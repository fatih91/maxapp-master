using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("SymptomSynonyms")]
    public class SymptomSynonym
    {
        public int SymptomSynonymId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        public int SymptomId { get; set; }
        
        public virtual Symptom Symptom { get; set; }
    }
}