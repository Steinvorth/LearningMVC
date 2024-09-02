using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ProjectoCursoWeb_BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")] //This is the area, its needed to identify the controller
    public class CategorytController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public CategorytController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }        
        public IActionResult Index()
        {
            return View();
        }
    }
}
