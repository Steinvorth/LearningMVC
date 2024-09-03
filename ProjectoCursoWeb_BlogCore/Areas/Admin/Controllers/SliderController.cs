using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ProjectoCursoWeb_BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SliderController(IWorkContainer workContainer, IWebHostEnvironment hostingEnvironment)
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            ModelState.Remove("ImageURL");
            if (ModelState.IsValid)
            {
                string route = _hostingEnvironment.WebRootPath;
                var files_ = HttpContext.Request.Form.Files;

                // Debug: Check if files were uploaded
                if (files_.Count() > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(route, @"images\slider");
                    var extension = Path.GetExtension(files_[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files_[0].CopyTo(fileStreams);
                    }

                    // Debug: Set ImageURL and CreationDate
                    slider.ImageURL = @"\images\slider\" + fileName + extension;

                    // Save the item to the database
                    _workContainer.SliderRepo.Add(slider);
                    _workContainer.save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Image", "Remember to select an Image.");
                }
            }

            return View(slider);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var slider = _workContainer.SliderRepo.Get(id.GetValueOrDefault());
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            ModelState.Remove("ImageURL");
            if (ModelState.IsValid)
            {
                string route = _hostingEnvironment.WebRootPath;
                var files_ = HttpContext.Request.Form.Files;
                var sliderDb = _workContainer.SliderRepo.Get(slider.Id);

                // Debug: Check if files were uploaded
                if (files_.Count() > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(route, @"images\slider");
                    var extension = Path.GetExtension(files_[0].FileName);
                    var newExtension = Path.GetExtension(files_[0].FileName);

                    var routeImage = Path.Combine(route, sliderDb.ImageURL.TrimStart('\\'));

                    if(System.IO.File.Exists(routeImage))
                    {
                        System.IO.File.Delete(routeImage);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files_[0].CopyTo(fileStreams);
                    }

                    // Debug: Set ImageURL and CreationDate
                    slider.ImageURL = @"\images\slider\" + fileName + extension;

                    // Save the item to the database
                    _workContainer.SliderRepo.Update(slider);
                    _workContainer.save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.ImageURL = sliderDb.ImageURL;
                }

                _workContainer.SliderRepo.Update(slider);
                _workContainer.save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }

        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _workContainer.SliderRepo.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var itemDbObj = _workContainer.SliderRepo.Get(id);
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

            _workContainer.SliderRepo.Remove(itemDbObj);
            _workContainer.save();
            return Json(new { success = true, message = "Item deleted successfully" });
        }

        #endregion
    }
}
