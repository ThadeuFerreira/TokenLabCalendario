using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenLabCalendar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(String FirstName, String LastName, String UserName, String Email)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.UserName = UserName;
            this.Email = Email;
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }

        public Profile Profile { get; set; }

    }
}
