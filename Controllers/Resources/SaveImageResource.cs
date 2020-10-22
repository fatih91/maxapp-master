using System;
using System.ComponentModel.DataAnnotations;

namespace maxapp.Controllers.Resources
{
    public class SaveImageResource
    {

   
        public string FileName { get; set; }
        public string ImageDescription { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
    }
}