using System.Collections.Generic;
using System.Collections.ObjectModel;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class PatientResource
    {
        public PatientResource()
        {
            Images = new Collection<Image>();
            Symptoms = new Collection<SaveSymptomResource>();
        }
        
        public int PatientId { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public string InitialDiagnosis { get; set; }
        public string Misc { get; set; }
        
        public ICollection<Image> Images { get; set; }
        public ICollection<SaveSymptomResource> Symptoms { get; set; }
        
        
    }
}