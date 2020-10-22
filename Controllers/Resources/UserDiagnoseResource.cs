using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace maxapp.Controllers.Resources
{
    public class UserDiagnoseResource
    {
        public UserDiagnoseResource()
        {
            Diagnoses = new Collection<UserContentResource>();
        }
          
        
        public bool Submitted { get; set; }
        public ICollection<UserContentResource> Diagnoses { get; set; }
    }
}