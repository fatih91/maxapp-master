using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using maxapp.Core.Models;

namespace maxapp.Controllers.Resources
{
    public class DifferentialdiagnoseResource
    {
   
        public int DiagnoseId { get; set; }
        public string TechnicalTerm { get; set; }
        public string Prevalence { get; set; }
        public string Icd { get; set; }
        public string Synonym { get; set; }

    }
}


