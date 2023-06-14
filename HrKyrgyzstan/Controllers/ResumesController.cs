using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HrKyrgyzstan.Context;
using HrKyrgyzstan.Entities;

namespace HrKyrgyzstan.Controllers
{
    public class ResumesController : Controller
    {
        private readonly AppDbContext _context;

        public ResumesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Resumes
        public async Task<IActionResult> Index()
        {
              return _context.Resume != null ? 
                          View(await _context.Resume.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Resume'  is null.");
        }

        // GET: Resumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resume == null)
            {
                return NotFound();
            }

            var resume = await _context.Resume
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // GET: Resumes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,PhoneNumber,WorkExperience,Skills,Certifications,References,Id")] Resume resume)
        {
            if (resume.Id == 0)
            {
                _context.Add(resume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resume);
        }

        // GET: Resumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resume == null)
            {
                return NotFound();
            }

            var resume = await _context.Resume.FindAsync(id);
            if (resume == null)
            {
                return NotFound();
            }
            return View(resume);
        }

        // POST: Resumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Email,PhoneNumber,WorkExperience,Skills,Certifications,References,Id")] Resume resume)
        {
            if (id != resume.Id)
            {
                return NotFound();
            }

            if (id == resume.Id)
            {
                try
                {
                    _context.Update(resume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResumeExists(resume.Id))
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
            return View(resume);
        }

        // GET: Resumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resume == null)
            {
                return NotFound();
            }

            var resume = await _context.Resume
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        // POST: Resumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resume == null)
            {
                return Problem("Entity set 'AppDbContext.Resume'  is null.");
            }
            var resume = await _context.Resume.FindAsync(id);
            if (resume != null)
            {
                _context.Resume.Remove(resume);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResumeExists(int id)
        {
          return (_context.Resume?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
