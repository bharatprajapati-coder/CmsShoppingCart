using CmsShoppingCart.Data;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly _DbContextClass dataContext;

        public PagesController(_DbContextClass dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet]
        //Get /Admin/Pages/
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in dataContext.Pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();
           
            
            
            ViewBag.message = TempData["msg"];
            ViewBag.Type  = TempData["type"];   
            return View(pagesList);

        }
        
        [HttpGet]
        //Get /Admin/Pages/Details/Id
        public async Task<IActionResult> Details(int id)
        {
            Page objPage = await dataContext.Pages.FirstOrDefaultAsync(o => o.Id == id);
            if(objPage == null) {
                return NotFound();
            }
            return View(objPage);   
        }


        //Post /Admin/Pages/Create
        public IActionResult Create() => View();


        //Post /Admin/Pages/Create/
        [HttpPost]
        public async Task<IActionResult> Create(Page obj)
        {
            ModelState.Clear();
            string message = null;

            if (ModelState.IsValid)
            {
                obj.Slug = obj.Title.ToLower().Replace(" " ,"-");
                obj.Sorting = 100;

                var slug = dataContext.Pages.FirstOrDefault(x => x.Slug == obj.Slug);
                if (slug != null)
                {
                    TempData["msg"]  = "the title is already exists!";
                    TempData["Type"] = "error";
                    return RedirectToAction("Index");
                
                }

                dataContext.Add(obj);
                await dataContext.SaveChangesAsync();
                TempData["msg"] = "Record Inserted  Successfully";
                TempData["type"] = "success";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        
        //Get /Admin/Pages/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var page = await dataContext.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);

        }

        //Post /Admin/Page/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Page obj)
        {
            if (ModelState.IsValid)
            {
                obj.Slug = obj.Id == 1 ? "home" : obj.Title.ToLower().Replace(" ", "-");
                 var slug = await dataContext.Pages.Where( x => x.Id != obj.Id).FirstOrDefaultAsync(x => x.Slug == obj.Slug);  
                if (slug != null)
                {
                    TempData["msg"] = "The page is already exists!";
                    TempData["Type"] = "error";
                    return View(obj);

                }

                dataContext.Update(obj);
                await dataContext.SaveChangesAsync();
                TempData["msg"] = "The page is updated successfully!";
                TempData["Type"] = "success";
                return RedirectToAction("Index");

            }

            return View(obj);
        }
    }
}
