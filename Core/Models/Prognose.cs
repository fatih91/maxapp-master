using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("Prognoses")]
    public class Prognose
    {
        public Prognose(){}
        
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        
        public int DiagnoseId { get; set; }
        
        public virtual Diagnose Diagnose { get; set; }
    }
}