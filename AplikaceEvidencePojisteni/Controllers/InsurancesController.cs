using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplikaceEvidencePojisteni.Data;
using AplikaceEvidencePojisteni.Models;

namespace AplikaceEvidencePojisteni.Controllers
{
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insurances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Insurance.Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Insurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: Insurances/Create
        public IActionResult Create(int userId)
        {
            ViewData["UserId"] = userId;
            return View();
        }

        // POST: Insurances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int userId, [Bind("InsuranceId,InsuranceType,Subject,Amount,ValidFrom,ValidUntil")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                // Nastavení UserId na zakladě parametrů
                insurance.UserId = userId;

                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Users", new { id = userId });
            }

            ViewData["UserId"] = userId;
            return View(insurance);
        }

        // GET: Insurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", insurance.UserId);
            return View(insurance);
        }

        // POST: Insurances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceId,InsuranceType,Subject,Amount,ValidFrom,ValidUntil")] Insurance insurance)
        {
            if (id != insurance.InsuranceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingInsurance = await _context.Insurance.FindAsync(id);
                    _context.Entry<Insurance>(existingInsurance).State = EntityState.Detached;

                    // Nastaví UserId na základě existující entity pojištění.
                    insurance.UserId = existingInsurance?.UserId ?? 0; // Provede explicitní převod z typu int? na int.

                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.InsuranceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Presměrovaní do view/user/detail
                return RedirectToAction("Details", "Users", new { id = insurance.UserId });
            }

            ViewData["UserId"] = new SelectList(_context.User, "UserId", "FirstName", insurance.UserId);
            return View(insurance);
        }

        // GET: Insurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // POST: Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insurance == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Insurance' is null.");
            }

            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }

            int userId = insurance.UserId; // Uloží ID uživatele před smazáním pojištění.

            _context.Insurance.Remove(insurance);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Users", new { id = userId });
        }

        private bool InsuranceExists(int id)
        {
          return (_context.Insurance?.Any(e => e.InsuranceId == id)).GetValueOrDefault();
        }
    }
}
