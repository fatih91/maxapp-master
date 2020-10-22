using System.Collections.Generic;
using System.Collections.ObjectModel;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class SaveSymptomResource
    {
        //Zum Aufrufen von Symptomen bei Diagnosen
        //und zum Speichern von Symptomen
        
        public SaveSymptomResource()
        
        {
            Synonyms = new Collection<KeyValuePairResource>();
            Diagnoses = new Collection<int>();
            Tags = new Collection<int>();
        }
        
        public int Id { get; set; }
        public string UserId { get; set; }
        public Modification Modification { get; set; }
        public string TechnicalTerm { get; set; }
        public string Definition { get; set; }
        public int CategoryId { get; set; }
        
        public ICollection<KeyValuePairResource> Synonyms { get; set; }
        public ICollection<int> Diagnoses { get; set; }
        public ICollection<int> Tags { get; set; }

    }
}