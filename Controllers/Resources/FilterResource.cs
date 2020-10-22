using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace maxapp.Controllers.Resources
{
    public class FilterResource
    {
        public string Q { get; set; }
        public ICollection<string> SearchTags { get; set; }

        public FilterResource()
        {
            SearchTags = new Collection<string>();
        }
    }
}