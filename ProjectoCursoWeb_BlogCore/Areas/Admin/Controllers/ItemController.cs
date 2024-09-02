using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ProjectoCursoWeb_BlogCore.Areas.Admin.Controllers
{
    public class ItemController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public ItemController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
