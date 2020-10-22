using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace maxapp.Controllers.Resources
{
    public class SavePatientResource
    {
        public int PatientId { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public string InitialDiagnosis { get; set; }
        public string Misc { get; set; }
        
        public ICollection<SaveImageResource> Images { get; set; }
        public ICollection<int> Symptoms { get; set; }

        public SavePatientResource()
        {
            Images = new Collection<SaveImageResource>();
            Symptoms = new Collection<int>();
        }
    }
}