using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjectoCursoWeb_BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public UserController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Get all Users
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var currentUser = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_workContainer.UserRepo.GetAll(u => u.Id != currentUser.Value));
        }

        [HttpGet]
        public IActionResult Block(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _workContainer.UserRepo.BlockUser(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Unblock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _workContainer.UserRepo.UnBlockUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
