using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("Checklists")]
    public class Checklist
    {
        public Checklist(){}
        
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Checkup { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }
        
        public int DiagnoseId { get; set; }
        
        public virtual Diagnose Diagnose { get; set; }
    }
}