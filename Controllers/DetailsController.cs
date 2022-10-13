using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    public class DetailsController : Controller
    {
        private readonly BikeContext _context;

        public IEnumerable<object> OrderDetails { get; private set; }

        public DetailsController(BikeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
            var bikeContext = _context.OrderDetails.Include(c => c.Product).Include(c => c.OrderMaster);
            return View(await bikeContext.ToListAsync());

        }
    }
}
