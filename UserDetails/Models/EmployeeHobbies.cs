using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserDetails.Models
{
    
    public class EmployeeHobbies
    {

        /*public int EmployeesHobbiesId { get; set; } // Add a unique identifier for the junction table*/

        // Foreign key to Employee

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int EmployeeHobbiesId {  get; set; } 
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employees { get; set; }
       
        
        public int HobbieId { get; set; }

        [ForeignKey("HobbieId")]
        public Hobbies Hobbies { get; set; }



    }
}