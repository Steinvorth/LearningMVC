using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProjectoCursoWeb_BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")] //This is the area, its needed to identify the controller
    public class CategoryController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public CategoryController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //Create
        [HttpGet]
        public IActionResult Create() //has to be named the same as the Action in the Index.cshtml
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category) //has to be named the same as the Action in the Index.cshtml
        {
            if(ModelState.IsValid)
            {
                _workContainer.CategoryRepo.Add(category);
                _workContainer.save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //Edit
        [HttpGet]
        public IActionResult Edit(int id) //has to be named the same as the Action in the Index.cshtml
        {
            Category category = new Category();
            category = _workContainer.CategoryRepo.Get(id);

            if(category != null)
            {
                return View(category);                
            }

            return NotFound();

        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data = _workContainer.CategoryRepo.GetAll()});
        }

        #endregion

    }
}
