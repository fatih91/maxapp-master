using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace maxapp.Core.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Diagnoses = new Collection<UserDiagnose>();
            Symptoms = new Collection<UserSymptom>();
        }

        public bool Submitted { get; set; }

        public ICollection<UserDiagnose> Diagnoses { get ; set; }
        public ICollection<UserSymptom> Symptoms { get ; set; }
        
             
    }
}