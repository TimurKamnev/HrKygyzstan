using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HrKyrgyzstan.Context;
using HrKyrgyzstan.Entities;

namespace HrKyrgyzstan.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Companies1
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Company.Include(c => c.City).Include(c => c.Country);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Companies1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Company == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .Include(c => c.City)
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies1/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            return View();
        }

        // POST: Companies1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,AboutUs,FeedBack,CountryId,CityId,Id")] Company company)
        {
            if (company.Id == 0)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", company.CityId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", company.CountryId);
            return View(company);
        }

        // GET: Companies1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Company == null)
            {
                return NotFound();
            }

            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", company.CityId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", company.CountryId);
            return View(company);
        }

        // POST: Companies1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,AboutUs,FeedBack,CountryId,CityId,Id")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (id == company.Id)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", company.CityId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", company.CountryId);
            return View(company);
        }

        // GET: Companies1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Company == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .Include(c => c.City)
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Company == null)
            {
                return Problem("Entity set 'AppDbContext.Company'  is null.");
            }
            var company = await _context.Company.FindAsync(id);
            if (company != null)
            {
                _context.Company.Remove(company);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
          return (_context.Company?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
