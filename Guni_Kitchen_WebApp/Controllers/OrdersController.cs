using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Guni_Kitchen_WebApp.Data;
using Guni_Kitchen_WebApp.Models;

namespace Guni_Kitchen_WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.Products).Include(o => o.Users);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Products)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.Order_Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["Product_Id"] = new SelectList(_context.Set<Product>(), "Product_Id", "Product_Description");
            ViewData["Id"] = new SelectList(_context.Users, "Id", "DisplayName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Order_Id,Order_Quantity,Order_Total,Order_Modes,Order_Status,Id,Product_Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Order_Id = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Product_Id"] = new SelectList(_context.Set<Product>(), "Product_Id", "Product_Description", order.Product_Id);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "DisplayName", order.Id);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["Product_Id"] = new SelectList(_context.Set<Product>(), "Product_Id", "Product_Description", order.Product_Id);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "DisplayName", order.Id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Order_Id,Order_Quantity,Order_Total,Order_Modes,Order_Status,Id,Product_Id")] Order order)
        {
            if (id != order.Order_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Order_Id))
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
            ViewData["Product_Id"] = new SelectList(_context.Set<Product>(), "Product_Id", "Product_Description", order.Product_Id);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "DisplayName", order.Id);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Products)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.Order_Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Order.Any(e => e.Order_Id == id);
        }
    }
}
