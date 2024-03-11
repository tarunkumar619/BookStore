    using BulkyWeb.Data;
    using BulkyWeb.Models;
    using Microsoft.AspNetCore.Mvc;

    namespace BulkyWeb.Controllers
    {
	    public class CategoryController : Controller
	    {

		    private readonly ApplicationDbContext _db;
            public CategoryController(ApplicationDbContext db)
            {
			
			    _db= db;
            
            }
            public IActionResult Index()
		    {
			    List<Category> categories = _db.Categories.ToList();
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
                    _db.Categories.Add(category);
                    _db.SaveChanges();
                TempData["Success"] = "success fully Created";
                return RedirectToAction("Index");
                }

			    return View();	
		    }
            public IActionResult Edit(int? id)
            {
                if (id == null) {
                    return NotFound();
                        }
                Category? categoryfrom = _db.Categories.FirstOrDefault(c => c.Id == id);
                if (categoryfrom==null)
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
                    _db.Categories.Update(category);
                    _db.SaveChanges();
                TempData["Success"] = "success fully edit";
                return RedirectToAction("Index");
                }

                return View();
            }



 
            public IActionResult Delete(int? id)

            {
                if(id==null|| id == 0)
                {
                    return NotFound();
                }
                Category? categoryform = _db.Categories.Find(id);

                if(categoryform==null)
                {
                    return NotFound();
                }
                return View(categoryform);
            }

            [HttpPost, ActionName("delete")]
            public IActionResult DeletePost(int id)
            {
                Category? obj=_db.Categories.Find(id);
                if (obj == null)
                {
                    return NotFound();
                }
                    _db.Categories.Remove(obj);
                    _db.SaveChanges();
            TempData["Success"] = "success fully delete";
                return RedirectToAction("Index");

         

            }

        }
    }
