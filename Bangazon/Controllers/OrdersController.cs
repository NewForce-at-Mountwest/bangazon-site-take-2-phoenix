using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Identity;
using Bangazon.Models.OrderViewModels;

namespace Bangazon.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        
     
        private readonly UserManager<ApplicationUser> _userManager;


        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Orders
        public async Task<IActionResult> Index()
        {   
            ApplicationUser user = await GetCurrentUserAsync();

            List<Order> orderList = await _context.Order
                .Include(o => o.PaymentType)
                .Where(client => client.UserId == user.Id)
                .ToListAsync();
            return View(orderList);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.PaymentType)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,DateCreated,DateCompleted,UserId,PaymentTypeId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber", order.PaymentTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var order = await _context.Order.FindAsync(id);
            //if (order == null)
            //{
            //    return NotFound();
            //}
            //ViewData["PaymentTypeId"] = new SelectList(_context.PaymentType, "PaymentTypeId", "AccountNumber", order.PaymentTypeId);
            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", order.UserId);
            //return View(order);
            if (id == null)
            {
                return NotFound();
            }

            var currentOrder = await _context.Order.FindAsync(id);
            if (currentOrder == null)
            {
                return NotFound();
            }

            ApplicationUser user = await GetCurrentUserAsync();
            currentOrder.UserId = user.Id;
            currentOrder.DateCompleted = DateTime.Now;


            OrderPaymentViewModel vm = new OrderPaymentViewModel()
            {
                FinalOrder = currentOrder,
                ThePaymentTypes = new SelectList(_context.PaymentType.Where(c => c.UserId == user.Id), "PaymentTypeId", "AccountNumber")
            };



            return View(vm);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderPaymentViewModel vm)
        {
            ModelState.Remove("FinalOrder.User");
            ModelState.Remove("FinalOrder.UserId");
            ApplicationUser user = await GetCurrentUserAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    vm.FinalOrder.UserId = user.Id;
                    _context.Update(vm.FinalOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(vm.FinalOrder.OrderId))
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

            vm.ThePaymentTypes = new SelectList(_context.PaymentType.Where(c => c.UserId == user.Id), "PaymentTypeId", "AccountNumber");

            
            return View(vm);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.PaymentType)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }


    }
}
