using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    public class MasterController : Controller
    {
        private readonly BikeContext _context;

        public IEnumerable<object> OrderMasters { get; private set; }

        public MasterController(BikeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var bikeContext = _context.OrderMasters;
            return View(await bikeContext.ToListAsync());

        }
        public async Task<IActionResult> Details(int? id)
        {
            
            ViewBag.name = HttpContext.Session.GetString("name");

           
             if (ViewBag.name != null)
            {
                if (id == null || _context.OrderMasters == null)
                {
                    return NotFound();
                }

                var master = await _context.OrderMasters
                    .Include(p => p.User)
                    .Include(q => q.OrderDetails)
                    .FirstOrDefaultAsync(m => m.OrderMasterid == id);
                if (master == null)
                {
                    return NotFound();
                }

                return View(master);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}

