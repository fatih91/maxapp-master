using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("DiagnoseSynonyms")]
    public class DiagnoseSynonym
    {
        public int DiagnoseSynonymId { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        public int DiagnoseId { get; set; }
        
        public virtual Diagnose Diagnose { get; set; }
    }
}