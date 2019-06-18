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

namespace Bangazon.Controllers
{
    public class PaymentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //GET current signed-in user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: PaymentTypes of current user
        public async Task<IActionResult> Index()
        {

            var currentUser = await GetCurrentUserAsync();
            var applicationDbContext = _context.PaymentType
                .Include(l => l.User)
                .Where(l => l.UserId == currentUser.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PaymentTypes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

       
        //    var paymentType = await _context.PaymentType
        //        .Include(p => p.User)
        //        .FirstOrDefaultAsync(m => m.PaymentTypeId == id);
        //    if (paymentType == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(paymentType);
        //}

        // GET: PaymentTypes/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentType paymentType)
        {
            ModelState.Remove("User");
            ModelState.Remove("UserId");



            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await GetCurrentUserAsync();
                paymentType.UserId = currentUser.Id;
                _context.Add(paymentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(paymentType);
        }

        // GET: PaymentTypes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    ApplicationUser currentUser = await GetCurrentUserAsync();

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var paymentType = await _context.PaymentType.FindAsync(id);
        //    if (paymentType == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["UserId"] = currentUser.Id;
        //    return View(paymentType);
        //}

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("PaymentTypeId,DateCreated,Description,AccountNumber,UserId")] PaymentType paymentType)
        //{
        //    if (id != paymentType.PaymentTypeId)
        //    {
        //        return NotFound();
        //    }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(paymentType);
//                    await _context.SaveChangesAsync();
//    }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!PaymentTypeExists(paymentType.PaymentTypeId))
//                    {
//                        return NotFound();
//}
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
        //    ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", paymentType.UserId);
        //    return View(paymentType);
        //}

        // GET: PaymentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .AsNoTracking()
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PaymentTypeId == id);
            if (paymentType == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see Russell Miller because he did this. Its probably because this Payment Type has been used on a completed Order";
            }



            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentType = await _context.PaymentType.FindAsync(id);
            if (paymentType == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.PaymentType.Remove(paymentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
         
        }

        //Check if Payment Type Eists

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentType.Any(e => e.PaymentTypeId == id);
        }
    }
}
