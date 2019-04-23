using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenLabCalendar.Models
{
    public class Event
    {

        [Key]
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public String Title { get; set; }
        public String Location { get; set; }

        public String Description { get; set; }

        public Profile Profile { get; set; }
    }
}
