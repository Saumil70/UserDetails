using UserDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserDetails.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BookStore.Areas.Admin.Controllers
{

/*    [Authorize(Roles=SD.Role_Admin)]*/
    public class CityController : Controller
    {
        private readonly EmployeeEntites _db;

        public CityController(EmployeeEntites db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<City> CityList = _db.Cities.Include(u=> u.States).ToList();
            return View(CityList);
        }

        public IActionResult Create()
        {
            var stateList = _db.States
            .Select(u => new SelectListItem
             {
              Text = u.StateName,
              Value = u.StateId.ToString()
             })
             .ToList(); // Ensure to execute the query

            ViewBag.StateList = stateList;

            return View();
        }


        [HttpPost]
        public IActionResult Create(City obj)

        {
           
            if (ModelState.IsValid)
            {
                _db.Cities.Add(obj);
                _db.SaveChanges();
                /*TempData["success"] = "Category addded successfully";*/
                return RedirectToAction("Index", "City");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var stateList = _db.States
            .Select(u => new SelectListItem
             {
             Text = u.StateName,
             Value = u.StateId.ToString()
             })
            .ToList(); // Ensure to execute the query

            ViewBag.StateList = stateList;

            City cityfromdb = _db.Cities.FirstOrDefault(u => u.CityId == id);
            if (cityfromdb == null)
            {
                return NotFound();
            }
            return View(cityfromdb);
        }

        [HttpPost]
        public IActionResult Edit(City obj)

        {

            if (ModelState.IsValid)
            {
                _db.Cities.Update(obj);
                _db.SaveChanges();
                /*TempData["success"] = "Category update successfully";*/
                return RedirectToAction("Index", "City");
            }
            return View();
        }


        /*        public IActionResult Delete(int? id)
                {
                    if (id == null || id == 0)
                    {
                        return NotFound();
                    }

                    Countries categoryfromdb = _db.Countries.FirstOrDefault(u => u.CountryId == id);
                    if (categoryfromdb == null)
                    {
                        return NotFound();
                    }
                    return View(categoryfromdb);
                }

                [HttpPost, ActionName("Delete")]
                public IActionResult DeletePost(int? id)

                {

                    Countries categoryfromdb = _db.Countries.FirstOrDefault(u => u.CountryId == id);

                    if (categoryfromdb == null)
                    {
                        return NotFound();
                    }

                    _db.Countries.Remove(categoryfromdb);
                    TempData["success"] = "Category deleted successfully";
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Country");
                }*/



        #region API CALLS   

        [HttpGet]
        public IActionResult GetAll()
        {
            var objCompanyList = _db.Cities.Include(u => u.States)
                .Select(u => new
                {
                    cityId = u.CityId,
                    cityName = u.CityName,
                    StateName = u.States.StateName
                });
            return Json(new { data = objCompanyList });
        }



        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var CityToBeDeleted = _db.Cities.FirstOrDefault(u => u.CityId == id);
            if (CityToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.Cities.Remove(CityToBeDeleted);
            _db.SaveChanges();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
