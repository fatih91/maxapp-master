using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class UserContentResource
    {    
        public int Id { get; set; }
        public string TechnicalTerm { get; set; }
        public string Synonym { get; set; }
        public string FileName { get; set; }
    }
}