using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using UserDetails.Models;


namespace UserDetails.ViewModel
{
    public class EmployeeViewModel
    {

            [ValidateNever]
            public Employee Employees { get; set; }

            [Display(Name = "Select Gender")]
            public int SelectedGenderId { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> GenderList { get; set; }


            [Display(Name = "Select Department")]   
            public int SelectedDepartmentId { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> DepartmentList { get; set; }

            [Display(Name = "Select Country")]
            public int SelectedCountryId { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> CountryList { get; set; }

            [Display(Name = "Select State")]   
            public int SelectedStateId { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> StateList { get; set; }

            [Display(Name = "Select City")]
            public int SelectedCityId { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> CityList { get; set; }

            [Display(Name = "Select Hobby")]
            public List<int> SelectedHobbies { get; set; }
            [ValidateNever]
            public MultiSelectList HobbyOptions { get; set; }
            [ValidateNever]    
            public EmployeeHobbies EmployeeHobbies { get; set; }




            public int EmployeeId { get; set; }
            public string Name {  get; set; }
            public string Email {  get; set; }  

            public string Address { get; set; }
            public bool IsActive { get; set; }  


    }
}