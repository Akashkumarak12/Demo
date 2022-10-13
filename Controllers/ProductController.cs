using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo.Models;
using Microsoft.AspNetCore.Http;

namespace Demo.Controllers
{
    public class ProductController : Controller
    {
        private readonly BikeContext _context;

        public ProductController(BikeContext context)
        {
            _context = context;
           
        }


        // GET: Product
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("Username");
            ViewBag.name = HttpContext.Session.GetString("name");

            if (ViewBag.UserName != null)
            {
                var bikeContext = _context.Product1s.Include(p => p.IdNavigation);
                ViewBag.ErrorMessage = "Out of Stock";
                return View(await bikeContext.ToListAsync());
            }
            else if (ViewBag.name != null)
            {
                var bikeContext = _context.Product1s.Include(p => p.IdNavigation);
                return View(await bikeContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("Username");
            ViewBag.name = HttpContext.Session.GetString("name");

            if (ViewBag.UserName != null)
            {
                if (id == null || _context.Product1s == null)
                {
                    return NotFound();
                }

                var product1 = await _context.Product1s
                    .Include(p => p.IdNavigation)
                    .FirstOrDefaultAsync(m => m.ProductId == id);
                //HttpContext.Session.SetInt32("Productid", product1.ProductId);
                //HttpContext.Session.SetInt32("Price", (int)product1.Price);
                if (product1 == null)
                {
                    return NotFound();
                }

                return View(product1);
            }
            else if (ViewBag.name != null)
            {
                if (id == null || _context.Product1s == null)
                {
                    return NotFound();
                }

                var product1 = await _context.Product1s
                    .Include(p => p.IdNavigation)
                    .FirstOrDefaultAsync(m => m.ProductId == id);
                if (product1 == null)
                {
                    return NotFound();
                }

                return View(product1);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewBag.name = HttpContext.Session.GetString("name");
             if (ViewBag.name != null)
            {
                ViewData["Id"] = new SelectList(_context.Categories, "Id", "Id");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Image,Brand,Price,Stock,CategoryId,Description,Id")] Product1 product1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Categories, "Id", "Id", product1.Id);
            return View(product1);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //ViewBag.UserName = HttpContext.Session.GetString("Username");
            ViewBag.name = HttpContext.Session.GetString("name");

            if (ViewBag.name != null)
            {
                if (id == null || _context.Product1s == null)
                {
                    return NotFound();
                }

                var product1 = await _context.Product1s.FindAsync(id);
                if (product1 == null)
                {
                    return NotFound();
                }
                ViewData["Id"] = new SelectList(_context.Categories, "Id", "Id", product1.Id);
                return View(product1);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Image,Brand,Price,Stock,CategoryId,Description,Id")] Product1 product1)
        {
            if (id != product1.Id)
            {
                return NotFound();
            }
            else
            {
                _context.Update(product1);
                await _context.SaveChangesAsync();
            }
            ViewData["Id"] = new SelectList(_context.Categories, "Id", "Id", product1.Id);
            return View(product1);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.name = HttpContext.Session.GetString("name");

            if (ViewBag.name != null)
            {
                if (id == null || _context.Product1s == null)
                {
                    return NotFound();
                }

                var product1 = await _context.Product1s
                    .Include(p => p.IdNavigation)
                    .FirstOrDefaultAsync(m => m.ProductId == id);
                if (product1 == null)
                {
                    return NotFound();
                }

                return View(product1);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product1s == null)
            {
                return Problem("Entity set 'BikeContext.Product1s'  is null.");
            }
            var product1 = await _context.Product1s.FindAsync(id);
            if (product1 != null)
            {
                _context.Product1s.Remove(product1);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> SearchStudentAsync(string Search, string SortOrder, int? PageNumber, string CurrentFilter)
        {
            ViewData["CurrentSort"] = SortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.UserName = HttpContext.Session.GetString("Username");
            ViewBag.name = HttpContext.Session.GetString("name");

            if (ViewBag.UserName != null)
            {

                if (Search != null)
                {
                    PageNumber = 1;
                }
                else
                {
                    Search = CurrentFilter;
                }


                ViewData["CurrentFilter"] = Search;

                var students = from s in _context.Product1s
                               select s;
                if (!string.IsNullOrEmpty(Search))
                {
                    students = students.Where(s => s.ProductName == Search);
                }

                //else if (!string.IsNullOrEmpty(Search))
                //{
                //    students = students.Where(s => s.Brand == Search);
                //}
                //else
                //{

                //}
                switch (SortOrder)
                {
                    case "name_desc":
                        students = students.OrderByDescending(s => s.ProductName);
                        break;
                    default:
                        students = students.OrderBy(s => s.ProductName);
                        break;
                }
                int pageSize = 3;
                return View(await PaginatedList<Product1>.CreateAsync(students.AsNoTracking(), PageNumber ?? 1, pageSize));
            }
            if (ViewBag.name != null)
            {

                if (Search != null)
                {
                    PageNumber = 1;
                }
                else
                {
                    Search = CurrentFilter;
                }


                ViewData["CurrentFilter"] = Search;

                var students = from s in _context.Product1s
                               select s;
                if (!string.IsNullOrEmpty(Search))
                {
                    students = students.Where(s => s.ProductName == Search);
                }

                //else if (!string.IsNullOrEmpty(Search))
                //{
                //    students = students.Where(s => s.Brand == Search);
                //}
                //else
                //{

                //}
                switch (SortOrder)
                {
                    case "name_desc":
                        students = students.OrderByDescending(s => s.ProductName);
                        break;
                    default:
                        students = students.OrderBy(s => s.ProductName);
                        break;
                }
                int pageSize = 3;
                return View(await PaginatedList<Product1>.CreateAsync(students.AsNoTracking(), PageNumber ?? 1, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public async Task<IActionResult> Buynow( int id)
        {
            var product1 = await _context.Product1s
                    .Include(p => p.IdNavigation)
                    .FirstOrDefaultAsync(m => m.ProductId == id);

            HttpContext.Session.SetInt32("Productid", product1.ProductId);
            HttpContext.Session.SetInt32("Price", (int)product1.Price);
         
                return RedirectToAction("Create", "Cart");
            
        }


        private bool Product1Exists(int id)
        {
          return _context.Product1s.Any(e => e.ProductId == id);
        }
    }
}
