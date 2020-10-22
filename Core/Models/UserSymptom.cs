using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("UserSymptom")]
    public class UserSymptom
    {
        public DateTime LastUpdate { get; set; }
        
        [EnumDataType(typeof(Modification))]
        public Modification Modification { get; set; }
        
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }

        
    }
}