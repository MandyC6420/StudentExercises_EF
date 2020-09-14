using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentExercises_EF.Data;
using StudentExercises_EF.Models;
using StudentExercises_EF.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises_EF.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Instructor.Include(i => i.Cohort);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructors = await _context.Instructor
                .Include(i => i.Cohort)
                .ThenInclude(c => c.assignedStudents)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructors == null)
            {
                return NotFound();
            }

            return View(instructors);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            InstructorCohortViewModel vm = new InstructorCohortViewModel();

            vm.cohorts = _context.Cohorts.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            vm.cohorts.Insert(0, new SelectListItem() { Value = "0", Text = "Please Choose a Cohort" });

            return View(vm);
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,SlackHandle,Specialty,CohortId")] Instructors instructors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CohortId"] = new SelectList(_context.Cohorts, "Id", "Id", instructors.CohortId);
            return View(instructors);

        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructors = await _context.Instructor.FindAsync(id);
            if (instructors == null)
            {
                return NotFound();
            }
            InstructorCohortViewModel vm = new InstructorCohortViewModel();

            vm.instructor = await _context.Instructor
               .FirstOrDefaultAsync(m => m.Id == id);

            vm.cohorts = _context.Cohorts.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            vm.cohorts.Insert(0, new SelectListItem() { Value = "0", Text = "Please Choose a Cohort" });

            return View(vm);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,SlackHandle,Specialty,CohortId")] Instructors instructors)
        {
            if (id != instructors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorsExists(instructors.Id))
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
            ViewData["CohortId"] = new SelectList(_context.Cohorts, "Id", "Name", instructors.CohortId);
            return View(instructors);
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructors = await _context.Instructor
                .Include(i => i.Cohort)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instructors == null)
            {
                return NotFound();
            }

            return View(instructors);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructors = await _context.Instructor.FindAsync(id);
            _context.Instructor.Remove(instructors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorsExists(int id)
        {
            return _context.Instructor.Any(e => e.Id == id);
        }
    }
}
