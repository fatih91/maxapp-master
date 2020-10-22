using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace maxapp.Controllers.Resources
{
    public class DiagnoseSymptomResource
    {
        //Zum Aufrufen von Symptomen bei Diagnosen
        //und zum Speichern von Symptomen
        
        public DiagnoseSymptomResource()
        
        {
            Synonyms = new Collection<KeyValuePairResource>();
        }
        
        public int Id { get; set; }
        public string TechnicalTerm { get; set; }
        public string Definition { get; set; }
        public string Synonym { get; set; }
        
        
        public ICollection<KeyValuePairResource> Synonyms { get; set; }

    }
}