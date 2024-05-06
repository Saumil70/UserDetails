using UserDetails.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserDetails.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace BookStore.Areas.Admin.Controllers
{

/*    [Authorize(Roles=SD.Role_Admin)]*/
    public class CountryController : Controller
    {
        private readonly EmployeeEntites _db;

        public CountryController(EmployeeEntites db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Countries> CountryList = _db.Countries.ToList();
            return View(CountryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Countries obj)

        {
           
            if (ModelState.IsValid)
            {
                _db.Countries.Add(obj);
                _db.SaveChanges();
                /*TempData["success"] = "Category addded successfully";*/
                return RedirectToAction("Index", "Country");
            }
            return View();
        }

        public IActionResult Edit(int? id)
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

        [HttpPost]
        public IActionResult Edit(Countries obj)

        {

            if (ModelState.IsValid)
            {
                _db.Countries.Update(obj);
                _db.SaveChanges();
                /*TempData["success"] = "Category update successfully";*/
                return RedirectToAction("Index", "Country");
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
            List<Countries> objCompanyList = _db.Countries.ToList();
            return Json(new { data = objCompanyList });
        }



        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _db.Countries.FirstOrDefault(u => u.CountryId == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.Countries.Remove(CompanyToBeDeleted);
            _db.SaveChanges();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
