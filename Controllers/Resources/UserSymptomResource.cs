using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace maxapp.Controllers.Resources
{
    public class UserSymptomResource
    {
        public UserSymptomResource()
        {
            Symptoms = new Collection<UserContentResource>();
        }
          
        
        public bool Submitted { get; set; }
        public ICollection<UserContentResource> Symptoms { get; set; }
    }
}