using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace ProjectoCursoWeb_BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ItemController(IWorkContainer workContainer, IWebHostEnvironment hostingEnvironment)
        {
            _workContainer = workContainer;
            _hostingEnvironment = hostingEnvironment;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemViewModel itemVM)
        {
            // Debug: Inspecting the incoming CategoryId
            Console.WriteLine($"Debug: Incoming CategoryId: {itemVM.Item.CategoryId}");

            // Remove potential issues from ModelState
            ModelState.Remove("Item.ImageURL");
            ModelState.Remove("Item.CreationDate");
            ModelState.Remove("CategoryList");
            ModelState.Remove("Item.Category"); // Remove Category from ModelState validation

            // Debug: Check ModelState validation before proceeding
            if (ModelState.IsValid)
            {
                Console.WriteLine("Debug: ModelState is valid.");

                string route = _hostingEnvironment.WebRootPath;
                var files_ = HttpContext.Request.Form.Files;

                // Debug: Check if files were uploaded
                if (itemVM.Item.Id == 0 && files_.Count() > 0)
                {
                    Console.WriteLine("Debug: File upload detected.");

                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(route, @"images\items");
                    var extension = Path.GetExtension(files_[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files_[0].CopyTo(fileStreams);
                    }

                    // Debug: Set ImageURL and CreationDate
                    itemVM.Item.ImageURL = @"\images\items\" + fileName + extension;
                    itemVM.Item.CreationDate = DateTime.Now.ToString();

                    Console.WriteLine($"Debug: ImageURL set to: {itemVM.Item.ImageURL}");
                    Console.WriteLine($"Debug: CreationDate set to: {itemVM.Item.CreationDate}");

                    // Save the item to the database
                    _workContainer.ItemRepo.Add(itemVM.Item);
                    _workContainer.save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Console.WriteLine("Debug: No file uploaded or model is not valid.");
                    ModelState.AddModelError("Image", "Remember to select an Image.");
                }
            }
            else
            {
                Console.WriteLine("Debug: ModelState is not valid.");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                    }
                }
            }

            // Debug: Re-populating the CategoryList for the view
            itemVM.CategoryList = _workContainer.CategoryRepo.GetCategoryList();
            Console.WriteLine("Debug: Re-populated CategoryList.");

            return View(itemVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var item = _workContainer.ItemRepo.Get(id.GetValueOrDefault());
            if (item == null)
            {
                return NotFound();
            }

            ItemViewModel itemVM = new ItemViewModel()
            {
                Item = item,
                CategoryList = _workContainer.CategoryRepo.GetCategoryList()
            };

            return View(itemVM);
        }


        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _workContainer.ItemRepo.GetAll(includeProperties: "Category");
            return Json(new { data = allObj });
        }

        #endregion
    }
}
