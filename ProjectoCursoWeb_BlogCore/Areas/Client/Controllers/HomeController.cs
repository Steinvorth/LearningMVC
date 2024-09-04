using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ProjectoCursoWeb_BlogCore.Models;
using System.Diagnostics;

namespace ProjectoCursoWeb_BlogCore.Areas.Client.Controllers
{
    //we need to add the specific area to the controller so it can be found
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public HomeController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Sliders = _workContainer.SliderRepo.GetAll(),
                Items = _workContainer.ItemRepo.GetAll()
            };

            ViewBag.IsHome = true;
            return View(homeViewModel);
        }

        //Search
        [HttpGet]
        public IActionResult SearchResult(string searchString, int page = 1, int pageSize = 6)
        {
            var items = _workContainer.ItemRepo.AsQueryable();

            if(!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString));
            }

            var itemsPaged = items.Skip((page -1) * pageSize).Take(pageSize);

            var model = new PaginatedList<Item>(itemsPaged.ToList(), items.Count(), page, pageSize, searchString);

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var dbItem = _workContainer.ItemRepo.Get(id);

            return View(dbItem);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
