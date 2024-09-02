using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ProjectoCursoWeb_BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        //Create
        [HttpGet]
        public IActionResult Create()
        {
            ItemViewModel itemVM = new ItemViewModel()
            {
                Item = new BlogCore.Models.Item(),
                CategoryList = _workContainer.CategoryRepo.GetCategoryList()
            };

            return View(itemVM);
        }

        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            //include properties allows to change the query to include the properties of the category or other tables.
            return Json(new { data = _workContainer.ItemRepo.GetAll(includeProperties: "Category") }); 
        }

        #endregion
    }
}
