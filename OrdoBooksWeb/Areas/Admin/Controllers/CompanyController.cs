using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdoBooks.DataAccsess.Repository;
using OrdoBooks.DataAccsess.Repository.IRepositroy;
using OrdoBooks.Model;
using OrdoBooks.Utility;

namespace OrdoBooksWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles =SD.Role_Admin)]
public class CompanyController : Controller
{
    public readonly IUnitofWork _iunitwork;
    public CompanyController(IUnitofWork unitofWork )
    {
        _iunitwork = unitofWork;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Upsert(int? id)
    {

        if (id == null || id == 0)
        {
            //create
            return View(new Company());
        }
        else
        {
            //update
            Company companyObj = _iunitwork.CompanyRepository.GetById(u => u.Id == id);
            return View(companyObj);
        }

    }
    [HttpPost]
    public IActionResult Upsert(Company CompanyObj)
    {
        if (ModelState.IsValid)
        {

            if (CompanyObj.Id == 0)
            {
                _iunitwork.CompanyRepository.Add(CompanyObj);
            }
            else
            {
                _iunitwork.CompanyRepository.Update(CompanyObj);
            }

            _iunitwork.save();
            TempData["success"] = "Company created successfully";
            return RedirectToAction("Index");
        }
        else
        {

            return View(CompanyObj);
        }
    }
    #region Api Calls

    public async Task<JsonResult> CompanyListataData()
    {
        var company = _iunitwork.CompanyRepository.GetAll().ToList();
        var Count = company.Count;
        var data = new
        {
            rows = company,
            Total = company.Count,
        };


        return Json(data);
    }
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var CompanyToBeDeleted = _iunitwork.CompanyRepository.GetById(u => u.Id == id);
        if (CompanyToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        _iunitwork.CompanyRepository.Remove(CompanyToBeDeleted);
        _iunitwork.save();

        return Json(new { success = true, message = "Delete Successful" });
    }
    #endregion

}
