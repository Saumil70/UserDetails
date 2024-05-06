using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserDetails.Models
{
    public class Hobbies
    {
        [Key]
        public int HobbieId { get; set; }
        public string HobbiesName { get; set; }

         public virtual List<EmployeeHobbies> EmployeeHobbies { get; set; }

    }
}