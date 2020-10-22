using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{ 
    [Table("UserDiagnose")]
    public class UserDiagnose
    {
        public DateTime LastUpdate { get; set; }
        
        [EnumDataType(typeof(Modification))]
        public Modification Modification { get; set; }

        
        public int DiagnoseId { get; set; }
        public Diagnose Diagnose { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        
    }
}