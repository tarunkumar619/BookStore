
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;




namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;    
        public ProductController(IUnitOfWork _unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            unitOfWork = _unitOfWork;
           _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> categories = unitOfWork.Product
            .GetAll(includeProperties: "category").ToList();

            return View(categories);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

        }

        [HttpPost]
            public IActionResult Upsert(ProductVM productVM, IFormFile? file)

            {

                if (ModelState.IsValid)
                {
                    string wwwRootPath=_webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string filename=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);  // name
                        string productpath = Path.Combine(wwwRootPath, "Images\\Product");  // location to save
                                                                                            // 

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl)) 
                    {
                      // delete old image
                      var oldImagePath=Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\')); 
                    
                        if(System.IO.File.Exists(oldImagePath)) {
                            System.IO.File.Delete(oldImagePath);
                        }
                                  
                    }

                        using (var fileStream = new FileStream(Path.Combine(productpath, filename), FileMode.Create))
                        {
                            file.CopyTo(fileStream);

                        } 
                        productVM.Product.ImageUrl = "\\Images\\Product\\"+filename;
                    }

                if (productVM.Product.Id == 0)
                {
                    unitOfWork.Product.Add(productVM.Product);
                    TempData["Success"] = "success fully Created";
                 

                }
                else
                {
                    unitOfWork.Product.Update(productVM.Product);
                    TempData["Success"] = "success fully Updated";
                }
       
                    unitOfWork.save();     
                    return RedirectToAction("Index");
                }
                return View();


            }


        //public IActionResult Delete(int? id)

        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? Productform = unitOfWork.
        //        Product.Get(c => c.Id == id);

        //    if (Productform == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Productform);
        //}

        //[HttpPost, ActionName("delete")]
        //public IActionResult DeletePost(int id)
        //{
        //    Product? obj = unitOfWork.Product.Get(c => c.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    unitOfWork.Product.Remove(obj);
        //    unitOfWork.save();
        //    TempData["Success"] = "success fully delete";
        //    return RedirectToAction("Index");

        //}


        #region API CAll
        public IActionResult GetAll()
        {
            List<Product> categories = unitOfWork.Product
            .GetAll(includeProperties: "category").ToList();

            return Json(categories);
        }


        [HttpDelete]
        [Route("Admin/Product/Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }


            unitOfWork.Product.Remove(productToBeDeleted);
            unitOfWork.save();

            return Json(new { success = true, message = "Delete Successful" });
        }




        #endregion
    }
}
