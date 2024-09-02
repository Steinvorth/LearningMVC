using BlogCore.AccesoDatos.Data.Repository.IRepository;
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

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new {data = _workContainer.CategoryRepo.GetAll()});
        }

        #endregion

    }
}
