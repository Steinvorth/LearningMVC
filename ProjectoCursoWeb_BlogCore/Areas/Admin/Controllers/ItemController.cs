using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ProjectoCursoWeb_BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ItemViewModel itemVM)
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
                string route = _hostingEnvironment.WebRootPath;
                var files_ = HttpContext.Request.Form.Files;

                var itemFromDb = _workContainer.ItemRepo.Get(itemVM.Item.Id);

                if (files_.Count() > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(route, @"images\items");
                    var extension = Path.GetExtension(files_[0].FileName);
                    var newExtension = Path.GetExtension(files_[0].FileName);

                    var imagePath = Path.Combine(route, itemFromDb.ImageURL.TrimStart('\\'));

                    if(System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    //upload the new file
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
                    _workContainer.ItemRepo.Update(itemVM.Item);
                    _workContainer.save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //item will just stay the same if there is an error.
                    itemVM.Item.ImageURL = itemFromDb.ImageURL;
                }

                _workContainer.ItemRepo.Update(itemVM.Item);
                _workContainer.save();

                return RedirectToAction(nameof(Index));
            }

            // Debug: Re-populating the CategoryList for the view
            itemVM.CategoryList = _workContainer.CategoryRepo.GetCategoryList();
            Console.WriteLine("Debug: Re-populated CategoryList.");

            return View(itemVM);
        }

        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _workContainer.ItemRepo.GetAll(includeProperties: "Category");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var itemDbObj = _workContainer.ItemRepo.Get(id);
            string route = _hostingEnvironment.WebRootPath;
            var routeImage = Path.Combine(route, itemDbObj.ImageURL.TrimStart('\\'));

            if (System.IO.File.Exists(routeImage))
            {
                System.IO.File.Delete(routeImage);
            }

            // if there is not a category with that id
            if (itemDbObj == null)
            {
                return Json(new { success = false, message = "Error while deleting item" });
            }

            _workContainer.ItemRepo.Remove(itemDbObj);
            _workContainer.save();
            return Json(new { success = true, message = "Item deleted successfully" });
        }

        #endregion
    }
}
