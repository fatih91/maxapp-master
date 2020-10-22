using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("Differentialdiagnoses")]
    public class Differentialdiagnose
    {
        public int DiagnoseId { get; set; }
        public int DifferentialDiagnoseId { get; set; }
        public Diagnose Diagnose { get; set; }
        public Diagnose DifferentialDiagnose { get; set; }
    }
}