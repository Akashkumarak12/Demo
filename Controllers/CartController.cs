using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo.Models;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.Controllers
{
    public class CartController : Controller
    {
        private readonly BikeContext _context;

        public IEnumerable<object> Carts { get; private set; }

        public CartController(BikeContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            
            var bikeContext = _context.Carts.Include(c => c.Product).Include(c => c.User);
            return View(await bikeContext.ToListAsync());

        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Cart/Create
        public IActionResult Create()
        {
            //ViewData["Productid"] = new SelectList(_context.Product1s, "ProductId", "ProductId");
            //ViewData["Userid"] = new SelectList(_context.User1s, "UserId", "UserId");
            
                return View();
            
        }

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,Quantity,TotalAmount")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                cart.Userid = (int?)HttpContext.Session.GetInt32("Userid");
                cart.Productid = (int)HttpContext.Session.GetInt32("Productid");
                int Amount = (int)HttpContext.Session.GetInt32("Price");
              
                    
                    cart.TotalAmount = cart.Quantity * Amount;
                    _context.Add(cart);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                //    }
            }
            //ViewData["Productid"] = new SelectList(_context.Product1s, "ProductId", "ProductId", cart.Productid);
            //ViewData["Userid"] = new SelectList(_context.User1s, "UserId", "UserId", cart.Userid);
            return View(cart);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            //ViewData["Productid"] = new SelectList(_context.Product1s, "ProductId", "ProductId", cart.Productid);
            //ViewData["Userid"] = new SelectList(_context.User1s, "UserId", "UserId", cart.Userid);
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,Quantity,TotalAmount,Userid,Productid")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cart.Userid = (int?)HttpContext.Session.GetInt32("Userid");
                    cart.Productid = (int)HttpContext.Session.GetInt32("Productid");
                    int Amount = (int)HttpContext.Session.GetInt32("Price");
                    cart.TotalAmount = cart.Quantity * Amount;
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Productid"] = new SelectList(_context.Product1s, "ProductId", "ProductId", cart.Productid);
            ViewData["Userid"] = new SelectList(_context.User1s, "UserId", "UserId", cart.Userid);
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'BikeContext.Carts'  is null.");
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
          return _context.Carts.Any(e => e.CartId == id);
        }
    }
}
