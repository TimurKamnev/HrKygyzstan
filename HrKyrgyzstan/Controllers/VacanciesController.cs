using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HrKyrgyzstan.Context;
using HrKyrgyzstan.Entities;
using Microsoft.AspNetCore.Identity;
using HrKyrgyzstan.Areas.Identity.Data;

namespace HrKyrgyzstan.Controllers
{
    public class VacanciesController : Controller
    {
        private readonly AppDbContext _context;

        public VacanciesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vacancies
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Vacancy.Include(v => v.City).Include(v => v.Company).Include(v => v.Country);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Vacancies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vacancy == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancy
                .Include(v => v.City)
                .Include(v => v.Company)
                .Include(v => v.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }

        // GET: Vacancies/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            return View();
        }

        // POST: Vacancies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,About,MinSalary,Responsibilities,YourHope,Skills,VacancyId,CompanyId,CountryId,CityId,Id")] Vacancy vacancy)
        {
            if (vacancy.Id == 0)
            {
                _context.Add(vacancy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", vacancy.CityId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", vacancy.CompanyId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", vacancy.CountryId);
            return View(vacancy);
        }

        // GET: Vacancies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vacancy == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancy.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", vacancy.CityId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", vacancy.CompanyId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", vacancy.CountryId);
            return View(vacancy);
        }

        // POST: Vacancies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,About,MinSalary,Responsibilities,YourHope,Skills,VacancyId,CompanyId,CountryId,CityId,Id")] Vacancy vacancy)
        {
            if (id != vacancy.Id)
            {
                return NotFound();
            }

            if (id == vacancy.Id)
            {
                try
                {
                    _context.Update(vacancy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacancyExists(vacancy.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Name", vacancy.CityId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", vacancy.CompanyId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", vacancy.CountryId);
            return View(vacancy);
        }

        // GET: Vacancies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vacancy == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancy
                .Include(v => v.City)
                .Include(v => v.Company)
                .Include(v => v.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }

        // POST: Vacancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vacancy == null)
            {
                return Problem("Entity set 'AppDbContext.Vacancy'  is null.");
            }
            var vacancy = await _context.Vacancy.FindAsync(id);
            if (vacancy != null)
            {
                _context.Vacancy.Remove(vacancy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacancyExists(int id)
        {
          return (_context.Vacancy?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
