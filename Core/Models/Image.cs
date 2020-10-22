using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maxapp.Core.Models
{
    [Table("Images")]
    public class Image
    {
        //public int ImageId { get; set; }
        
        [Key]
        [StringLength(255)]
        public string FileName { get; set; }
           
        [StringLength(255)]
        public string ImageDescription { get; set; }
        
        [Range(0,1)]
        public int Gender { get; set; }
        public int Age { get; set; }

        public int SymptomId { get; set; }
        
        public Symptom Symptom { get; set; }
        
    }
}