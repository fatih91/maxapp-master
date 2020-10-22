using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("DiagnoseSymptoms")]
    public class DiagnoseSymptom
    {
        public int DiagnoseId { get; set; }

        public int SymptomId { get; set; }

        public Diagnose Diagnose { get; set; }

        public Symptom Symptom { get; set; }
    }
}