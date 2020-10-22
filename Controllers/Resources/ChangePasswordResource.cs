using System.ComponentModel.DataAnnotations;

namespace maxapp.Controllers.Resources
{
    public class ChangePasswordResource
    {
       
        public string Name { get; set; }

        public string NewPassword { get; set; }

        public string OldPassword { get; set; }
       
    }
}