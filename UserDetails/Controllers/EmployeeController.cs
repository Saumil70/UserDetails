using UserDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserDetails.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Employees.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly EmployeeEntites _db;

        public EmployeeController(EmployeeEntites db)
        {
            _db = db;
        }


        public ActionResult Index()
        
        
        
        {
            var employees = _db.Employees.Include(u=> u.Department).Include(u=> u.Gender).Include(u=> u.Country).Include(u=> u.States).Include(u=> u.Cities).ToList();
            foreach (var employee in employees)
            {
                _db.Entry(employee)
                    .Collection(s => s.EmployeeHobbies)
                    .Query().Include(sh => sh.Hobbies).Load();
            }

            return View(employees);


        }

        private MultiSelectList GetHobbyOptions(List<int> selectedHobbies = null)
        {
            /*using (var context = new EmployeeEntites())*/
            {
                var hobbies = _db.Hobbies.ToList();

                return new MultiSelectList(hobbies, "HobbieId", "HobbiesName", selectedHobbies);
            }
        }


        [HttpGet]
        public ActionResult CreateOrEdit(int? id)
        {
            // Common code for both create and edit
            var viewModel = new EmployeeViewModel
            {
                // Populate dropdown lists
                GenderList = _db.Genders.Select(g => new SelectListItem { Value = g.GenderName.ToString(), Text = g.GenderName }).ToList(),
                DepartmentList = _db.Departments.Select(d => new SelectListItem { Value = d.DepartmentId.ToString(), Text = d.DepartmentName }).ToList(),
                CountryList = _db.Countries.Select(c => new SelectListItem { Value = c.CountryId.ToString(), Text = c.CountryName }).ToList(),
                StateList = _db.States.Select(s => new SelectListItem {Value = s.StateId.ToString(), Text = s.StateName}).ToList(),
                CityList = _db.Cities.Select(s => new SelectListItem { Value = s.CityId.ToString(), Text = s.CityName }).ToList(),

                HobbyOptions = GetHobbyOptions(),
                SelectedHobbies = new List<int>()


                // Make sure it's initialized as an empty list





            };

            if (id.HasValue)
            {

                var existingStudents = _db.Employees.Include(e => e.EmployeeHobbies).SingleOrDefault(e => e.EmployeeId == id); ;
                if (existingStudents == null)
                {
                    return NotFound();
                }


                viewModel.EmployeeId = existingStudents.EmployeeId;
                viewModel.Name = existingStudents.Name;
                viewModel.Email = existingStudents.Email;
                viewModel.Address = existingStudents.Address;
                viewModel.SelectedGenderId = existingStudents.GenderId;
                viewModel.IsActive = existingStudents.IsActive;
                viewModel.SelectedCountryId = existingStudents.CountryId;
                viewModel.SelectedStateId = existingStudents.StateId;
                viewModel.SelectedCityId = existingStudents.CityId;
                viewModel.SelectedDepartmentId = existingStudents.DepartmentId;

                viewModel.SelectedHobbies = existingStudents.EmployeeHobbies.Select(s => s.HobbieId).ToList();
            }
            // Assuming you have a Hobby model and a Hobbies table in the database





            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.EmployeeId > 0)
                {
                    // Edit existing employee
                    var existingEmployee = _db.Employees.Include(e => e.EmployeeHobbies)
                            .SingleOrDefault(e => e.EmployeeId == viewModel.EmployeeId);
                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }


                    existingEmployee.Name = viewModel.Name;
                    existingEmployee.Email = viewModel.Email;
                    existingEmployee.Address = viewModel.Address;
                    existingEmployee.GenderId = viewModel.SelectedGenderId;
                    existingEmployee.IsActive = viewModel.IsActive;
                    existingEmployee.CountryId = viewModel.SelectedCountryId;
                    existingEmployee.StateId = viewModel.SelectedStateId;
                    existingEmployee.CityId = viewModel.SelectedCityId;

                    /* existingEmployees.EmployeeHobbies.Clear();*/
                    var extHobbies = existingEmployee.EmployeeHobbies.Where(x => x.EmployeeId == viewModel.EmployeeId).ToList();
                    foreach (var item in extHobbies)
                    {
                        _db.EmployeeHobbies.Remove(item);
                    }
                    if (viewModel.SelectedHobbies != null && viewModel.SelectedHobbies.Any())
                    {
                        foreach (var hobbyId in viewModel.SelectedHobbies)
                        {
                            existingEmployee.EmployeeHobbies.Add(new EmployeeHobbies { HobbieId = hobbyId, EmployeeId = viewModel.EmployeeId });
                        }
                    }
                    _db.SaveChanges();
                }
                else
                {

                    var newEmployee = new Employee
                    {
                        Name = viewModel.Name,
                        Email = viewModel.Email,
                        Address = viewModel.Address,
                        IsActive = true,
                        CountryId = viewModel.SelectedCountryId,
                        GenderId = viewModel.SelectedGenderId,
                        DepartmentId = viewModel.SelectedDepartmentId,
                        StateId = viewModel.SelectedStateId,
                        CityId = viewModel.SelectedCityId



                    };
                    if (viewModel.SelectedHobbies != null && viewModel.SelectedHobbies.Any())
                    {
                        newEmployee.EmployeeHobbies = viewModel.SelectedHobbies
                            .Select(h => new EmployeeHobbies { HobbieId = h })
                            .ToList();
                    }

                    _db.Employees.Add(newEmployee);
                }

                // Save changes to the database
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            // If ModelState is not valid, re-populate dropdown lists and return to the CreateOrEdit view
            viewModel.GenderList = _db.Genders.Select(g => new SelectListItem { Value = g.GenderName.ToString(), Text = g.GenderName }).ToList();
            viewModel.DepartmentList = _db.Departments.Select(d => new SelectListItem { Value = d.DepartmentId.ToString(), Text = d.DepartmentName }).ToList();
            viewModel.CountryList = _db.Countries.Select(c => new SelectListItem { Value = c.CountryId.ToString(), Text = c.CountryName }).ToList();
            viewModel.StateList = _db.States.Select(c => new SelectListItem { Value = c.StateId.ToString(), Text = c.StateName }).ToList();
            viewModel.CityList = _db.Cities.Select(c => new SelectListItem { Value = c.CityId.ToString(), Text = c.CityName }).ToList();

            viewModel.HobbyOptions = GetHobbyOptions(viewModel.SelectedHobbies);
            return View(viewModel);
        }





        /*[HttpPost]
        [ValidateAntiForgeryToken]*/
        public ActionResult Delete(int id)
        {
            // Retrieve the employee by ID
            Employee employeeToDelete = _db.Employees.Find(id);

            if (employeeToDelete == null)
            {
                // Handle the case where the employee is not found
                return NotFound();
            }

            // Find and delete records from EmployeeHobbies associated with the employee
            var employeeHobbiesToDelete = _db.EmployeeHobbies.Where(eh => eh.EmployeeId == id);
            foreach (var employeeHobby in employeeHobbiesToDelete)
            {
                _db.EmployeeHobbies.Remove(employeeHobby);
            }

            // Remove the employee from the database

            _db.Employees.Remove(employeeToDelete);
            _db.SaveChanges();

            return RedirectToAction("Index"); // Redirect to the employee list or another appropriate action
        }


        public IActionResult GetStates(int countryId)
        {
            // Retrieve states for the selected country from the database
            var states = _db.States.Where(u => u.CountryId == countryId);
            return Json(states); // Return states as JSON
        }


        [HttpGet]
        public IActionResult GetCities(int stateId)
        {
            // Retrieve states for the selected country from the database
            var cities = _db.Cities.Where(u => u.StateId == stateId);
            return Json(cities); // Return states as JSON
        }

    }
}