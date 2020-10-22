
using System.Collections.Generic;

namespace maxapp.Controllers.Resources
{
    public class TagDiagnoseResource
    {
        public int DiagnoseId { get; set; }
        public string TechnicalTerm { get; set; }

        public ICollection<KeyValuePairResource> Synonyms { get; set; }
        public ICollection<KeyValuePairResource> Icds { get; set; }
    }
}


