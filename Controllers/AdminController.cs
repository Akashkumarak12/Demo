using Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class AdminController : Controller
    {
        private readonly BikeContext db;
        private readonly ISession session;
        public AdminController(BikeContext _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admi obj)
        {
            var result = (from i in db.Admis
                          where i.Username == obj.Username && i.Password == obj.Password
                          select i).SingleOrDefault();
            if (result != null)
            {
                HttpContext.Session.SetString("name", result.Username);
                return RedirectToAction("Index", "Product");
            }
            else
                return View();
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
