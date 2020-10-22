using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("SymptomTagAssignments")]
    public class SymptomTagAssignment
    {
        public int SymptomId { get; set; }

        public int TagId { get; set; }

        public Symptom Symptom { get; set; }

        public Tag Tag { get; set; }
    }
}