using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace maxapp.Controllers.Resources
{
    public class SymptomDiagnoseResource
    {

        public SymptomDiagnoseResource()
        {
            Synonyms = new Collection<KeyValuePairResource>();
            Icds = new Collection<KeyValuePairResource>();
        }
        
        public int DiagnoseId { get; set; }
        public string TechnicalTerm { get; set; }
        public string Prevalence { get; set; }

        public ICollection<KeyValuePairResource> Icds { get; set; }
        public ICollection<KeyValuePairResource> Synonyms { get; set; }
        
    }
}