using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace maxapp.Controllers.Resources
{
    public class TagResource
    {
        public TagResource()
        {
            Diagnoses = new Collection<TagDiagnoseResource>();
            Symptoms = new Collection<TagSymptomResource>();
        }
        
        public int TagId { get; set; }

        public string Name { get; set; }

        public ICollection<TagDiagnoseResource> Diagnoses { get; set; }

        public ICollection<TagSymptomResource> Symptoms { get; set; }
    }
}