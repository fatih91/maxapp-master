using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace maxapp.Controllers.Resources
{
    public class TagSymptomResource
    {
        public TagSymptomResource()
        {
            Synonyms = new Collection<KeyValuePairResource>();
        }
        
        public int SymptomId { get; set; }
        public string TechnicalTerm { get; set; }

        public ICollection<KeyValuePairResource> Synonyms { get; set; }
    }
}