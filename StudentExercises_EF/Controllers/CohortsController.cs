using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentExercises_EF.Data;
using StudentExercises_EF.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises_EF.Controllers
{
    public class CohortsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CohortsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cohorts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cohorts.ToListAsync());
        }

        // GET: Cohorts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cohorts = await _context.Cohorts
                .Include(c => c.assignedStudents)
                .Include(c => c.InstructorList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cohorts == null)
            {
                return NotFound();
            }

            return View(cohorts);
        }

        // GET: Cohorts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cohorts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Cohorts cohorts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cohorts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cohorts);
        }

        // GET: Cohorts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cohorts = await _context.Cohorts.FindAsync(id);
            if (cohorts == null)
            {
                return NotFound();
            }
            return View(cohorts);
        }

        // POST: Cohorts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Cohorts cohorts)
        {
            if (id != cohorts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cohorts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CohortsExists(cohorts.Id))
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
            return View(cohorts);
        }

        // GET: Cohorts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cohorts = await _context.Cohorts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cohorts == null)
            {
                return NotFound();
            }

            return View(cohorts);
        }

        // POST: Cohorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cohorts = await _context.Cohorts.FindAsync(id);
            _context.Cohorts.Remove(cohorts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CohortsExists(int id)
        {
            return _context.Cohorts.Any(e => e.Id == id);
        }
    }
}
