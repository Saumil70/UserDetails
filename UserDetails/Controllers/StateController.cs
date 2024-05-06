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
    public class StateController : Controller
    {
        private readonly EmployeeEntites _db;

        public StateController(EmployeeEntites db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<State> StateList = _db.States.Include(u=> u.Country).ToList();
            return View(StateList);
        }

        public IActionResult Create()
        {
            var countryList = _db.Countries
            .Select(u => new SelectListItem
             {
              Text = u.CountryName,
              Value = u.CountryId.ToString()
             })
             .ToList(); // Ensure to execute the query

            ViewBag.CountryList = countryList;

            return View();
        }


        [HttpPost]
        public IActionResult Create(State obj)

        {
           
            if (ModelState.IsValid)
            {
                _db.States.Add(obj);
                _db.SaveChanges();
                /*TempData["success"] = "Category addded successfully";*/
                return RedirectToAction("Index", "State");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var countryList = _db.Countries
            .Select(u => new SelectListItem
             {
             Text = u.CountryName,
             Value = u.CountryId.ToString()
             })
            .ToList(); // Ensure to execute the query

            ViewBag.CountryList = countryList;

            State statefromdb = _db.States.FirstOrDefault(u => u.StateId == id);
            if (statefromdb == null)
            {
                return NotFound();
            }
            return View(statefromdb);
        }

        [HttpPost]
        public IActionResult Edit(State obj)

        {

            if (ModelState.IsValid)
            {
                _db.States.Update(obj);
                _db.SaveChanges();
                /*TempData["success"] = "Category update successfully";*/
                return RedirectToAction("Index", "State");
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
            var objCompanyList = _db.States.Include(u => u.Country)
                .Select(u => new
                {
                    stateId = u.StateId,
                    stateName = u.StateName,
                    countryName = u.Country.CountryName
                });
            return Json(new { data = objCompanyList });
        }



        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var StateToBeDeleted = _db.States.FirstOrDefault(u => u.StateId == id);
            if (StateToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.States.Remove(StateToBeDeleted);
            _db.SaveChanges();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
