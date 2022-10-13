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
using Microsoft.AspNetCore.Http;

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
            int id = (int)HttpContext.Session.GetInt32("Userid");
            var bikeContext = _context.Carts.Include(c => c.Product).Include(c => c.User);
            List<Cart> cart = (from i in _context.Carts where i.Userid == id select i).ToList();
            if (cart.Count == 0)
            {
                ViewBag.ErrorMessage = "Cart is Empty";
                return View(await bikeContext.ToListAsync());
            }
            return View(await bikeContext.ToListAsync());
        }

        // GET: Cart/Details/5
        [HttpPost]
        public async Task<IActionResult> ProceedtoBuy()
        {
            var UserId = HttpContext.Session.GetInt32("Userid");
            List<Cart> cart = (from i in _context.Carts where i.Userid == UserId select i).ToList();
            List<OrderDetail> od = new List<OrderDetail>();
           
            OrderMaster om = new OrderMaster();

            om.Orderdate = DateTime.Today;
            om.Userid = (int)UserId;
            om.TotalAmount = 0;
            foreach (var item in cart)
            {

                om.TotalAmount += (int)item.TotalAmount;
            }
            _context.Add(om);

            _context.SaveChanges();
            HttpContext.Session.SetInt32("Total", (int)om.TotalAmount);
            foreach (var item in cart)
            {
                OrderDetail detail = new OrderDetail();
                detail.Productid = item.Productid;
                detail.Userid=item.Userid;
                detail.TotalAmount = item.TotalAmount;
                detail.OrderMasterid = om.OrderMasterid;
                od.Add(detail);
            }
            _context.AddRange(od);
            _context.SaveChanges();
          


            return RedirectToAction("GetPayment", new { id = om.OrderMasterid });

        }

        // GET: Cart/Create
        [HttpGet]
        public IActionResult Create()
        {
            Cart cart = new Cart();
            //ViewData["Productid"] = new SelectList(_context.Product1s, "ProductId", "ProductId");
            //ViewData["Userid"] = new SelectList(_context.User1s, "UserId", "UserId");
            cart.Quantity = 1;
            return View(cart);
        }

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cart cart)
        {

            cart.Userid = (int?)HttpContext.Session.GetInt32("Userid");
            cart.Productid = (int)HttpContext.Session.GetInt32("Productid");
            var id = (from i in _context.Carts
                      where i.Userid == cart.Userid && i.Productid == cart.Productid
                      select i).SingleOrDefault();
            if (id == null)
            {
                int Amount = (int)HttpContext.Session.GetInt32("Price");
                cart.TotalAmount = cart.Quantity * Amount;
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                id.Quantity += cart.Quantity;
                id.TotalAmount = id.Quantity * (from i in _context.Carts
                                                where i.Productid == cart.Productid
                                                select i.Product.Price).SingleOrDefault();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //_context.Add(cart);
            //await _context.SaveChangesAsync();

            //    }

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

            var cart = _context.Carts.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,CartId,Quantity,TotalAmount,Userid,Productid")]Cart cart)
        {
            if (id != cart.Id)
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
                    if (!CartExists(cart.Id))
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
            //ViewData["Productid"] = new SelectList(_context.Product1s, "ProductId", "ProductId", cart.Productid);
            //ViewData["Userid"] = new SelectList(_context.User1s, "UserId", "UserId", cart.Userid);
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public IActionResult GetPayment(int id)
        {
            var OrderMaster = _context.OrderMasters.Find(id);
            return View(OrderMaster);
        }


        [HttpPost]
        public IActionResult GetPayment(OrderMaster m)
        {


            if (m.AmountPaid == m.TotalAmount)
            {
                var UserId = HttpContext.Session.GetInt32("Userid");
                List<Cart> cart = (from i in _context.Carts where i.Userid == UserId select i).ToList();
                var pid= (int)HttpContext.Session.GetInt32("Productid");
               
                Product1 p = new Product1();
                var s = (from i in _context.Product1s
                             where i.ProductId == pid
                             select i).SingleOrDefault();
                var c = (from t in _context.Carts
                         where t.Productid == pid
                         select t).SingleOrDefault();
                _context.OrderMasters.Update(m);
                _context.SaveChanges();
                s.Stock -= c.Quantity;
                _context.Product1s.Update(s);
                _context.SaveChanges();
                _context.Carts.RemoveRange(cart);
                _context.SaveChanges();


                return RedirectToAction("Thankyou");
            }
            else
            {
                ViewBag.ErrorMessage = "amount not valid";
                return View(m);
            }

        }
    
        public IActionResult Thankyou()
        {
            return View();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}

