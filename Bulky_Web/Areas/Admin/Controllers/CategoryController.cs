
using BulkyBook.DataAccess.Context;
using BulkyBook.DataAccess.Repository;
using BulkyBook.Models1;
using BulkyBookDataAccess.Repository.UintOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUintOfWork _uintOfWork;
        public CategoryController(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;

        }

        public IActionResult Index()
        {
            List<Category> category = _uintOfWork.Category.GetAll().ToList();

            return View(category);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {

                _uintOfWork.Category.Add(category);
                _uintOfWork.Save();
                TempData["Sucsess"] = "sucsess tis update";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = _uintOfWork.Category.Get(u => u.Id == id);
            Category category1 = _uintOfWork.Category.Get(x => x.Id == id);


            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _uintOfWork.Category.UpDate(category);
                _uintOfWork.Save();
                TempData["Sucsess"] = "sucsess tis update";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Delete(int id)
        {
            Category category = _uintOfWork.Category.Get(u => u.Id == id);
            _uintOfWork.Category.Delete(category);
            _uintOfWork.Save();
            TempData["Sucsess"] = "sucsess tis update";
            return RedirectToAction("Index");

        }
    }
}
