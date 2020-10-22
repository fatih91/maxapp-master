using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("DiagnoseTagAssignments")]
    public class DiagnoseTagAssignment
    {
        public int DiagnoseId { get; set; }

        public int TagId { get; set; }

        public Diagnose Diagnose { get; set; }

        public Tag Tag { get; set; }
    }
}