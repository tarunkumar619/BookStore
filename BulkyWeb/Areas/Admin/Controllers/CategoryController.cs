
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = unitOfWork.Category
            .GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)

        {
            if (category.Name.ToLower() == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "this displayOder cannot exactly match the name");
            }

            if (ModelState.IsValid)
            {

                unitOfWork.Category
                    .Add(category);
                unitOfWork.save();

                TempData["Success"] = "success fully Created";
                return RedirectToAction("Index");
            }

            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? categoryfrom = unitOfWork.Category.Get(c => c.Id == id);

            if (categoryfrom == null)
            {
                return NotFound();
            }
            return View(categoryfrom);
        }

        [HttpPost]
        public IActionResult Edit(Category category)

        {

            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(category);
                unitOfWork.save();
                TempData["Success"] = "success fully Udate";
                return RedirectToAction("Index");
            }

            return View();
        }




        public IActionResult Delete(int? id)

        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryform = unitOfWork.
                Category.Get(c => c.Id == id);

            if (categoryform == null)
            {
                return NotFound();
            }
            return View(categoryform);
        }

        [HttpPost, ActionName("delete")]
        public IActionResult DeletePost(int id)
        {
            Category? obj = unitOfWork.Category.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            unitOfWork.Category.Remove(obj);
            unitOfWork.save();
            TempData["Success"] = "success fully delete";
            return RedirectToAction("Index");

        }

    }
}
