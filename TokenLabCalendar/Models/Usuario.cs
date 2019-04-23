using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TokenLabCalendar.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Password { get; set; }
    }
}
