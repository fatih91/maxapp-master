using System.Collections.Generic;
using System.Collections.ObjectModel;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class CategorySymptomResource
    {
        public int SymptomId { get; set; }
        public string TechnicalTerm { get; set; }
        public string FileName { get; set; }
        public string Synonym { get; set; }


    }
}