using System.Collections.Generic;
using System.Collections.ObjectModel;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class SymptomResource
    {
        public SymptomResource()
        {
            Diagnoses = new Collection<SymptomDiagnoseResource>();
            Tags = new Collection<DSTagResource>();
            Images = new Collection<ImageResource>();
            Synonyms = new Collection<KeyValuePairResource>();
        }
        public int SymptomId { get; set; }
        public string TechnicalTerm { get; set; }
        public string Definition { get; set; }
        public SymptomCategoryResource Category { get; set; }

        public ICollection<KeyValuePairResource> Synonyms { get; set; }
        public ICollection<SymptomDiagnoseResource> Diagnoses { get; set; }
        public ICollection<DSTagResource> Tags { get; set; }
        public ICollection<ImageResource> Images { get; set; }
    }
}