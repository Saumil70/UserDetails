using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UserDetails.Models;

namespace UserDetails.Models
{
    public class Employee
    {
        public Employee()
        {
            this.EmployeeHobbies = new List<EmployeeHobbies>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public bool IsActive {  get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public int CountryId {  get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int CityId { get; set; } 



        // Navigation property for Department
        [ForeignKey("DepartmentId")]
        [ValidateNever]
        public virtual Departments Department { get; set; }

        // Navigation property for Gender
        [ForeignKey("GenderId")]
        [ValidateNever]
        public virtual Genders Gender { get; set; }

        // Navigation property for Country
        [ForeignKey("CountryId")]
        [ValidateNever]
        public virtual Countries Country { get; set; }

        // Navigation property for Country
        [ForeignKey("StateId")]
        [ValidateNever]
        public virtual State States { get; set; }

        // Navigation property for Country
        [ForeignKey("CityId")]
        [ValidateNever]
        public virtual City Cities { get; set; }

        [ValidateNever]
        public List<EmployeeHobbies> EmployeeHobbies { get; set; }



    }
}