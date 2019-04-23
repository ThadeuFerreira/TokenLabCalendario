using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TokenLabCalendar.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String ProfileDescription { get; set; }
        public ICollection<Event> Events { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserForeignKey { get; set; }
    }
}
