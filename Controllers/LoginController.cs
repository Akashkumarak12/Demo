using Microsoft.AspNetCore.Mvc;
using Demo.Models;


namespace Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly BikeContext db;
        private readonly ISession session;
        public LoginController(BikeContext _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User1 obj)
        {
            var result = (from i in db.User1s
                          where i.EmailId == obj.EmailId && i.Password == obj.Password 
                          select i).SingleOrDefault();
            if (result != null)
            {
                HttpContext.Session.SetString("Username", result.UserName);
                HttpContext.Session.SetInt32("Userid", result.UserId);
                return RedirectToAction("Index", "Product");
            }
            else
                return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User1 obj)
        {
            db.User1s.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
