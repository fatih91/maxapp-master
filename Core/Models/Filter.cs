using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace maxapp.Core.Models
{
    public class Filter
    {

        public Filter()
        {
            SearchTags = new Collection<string>();
        }

        public string SearchTerm { get; set; }
        public string Icd { get; set; }
        public IEnumerable<string> SearchTags { get; set; }
        
        
    }
}