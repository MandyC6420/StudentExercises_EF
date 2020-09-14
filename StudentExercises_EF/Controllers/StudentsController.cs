using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentExercises_EF.Data;
using StudentExercises_EF.Models;
using StudentExercises_EF.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercises_EF.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Students.Include(s => s.Cohort);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Cohort)
                .Include(s => s.assignedExercises)
                    .ThenInclude(ae => ae.Exercise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // GET: Students/Create
        public async Task<IActionResult> Create()
        {
            List<Cohorts> cohorts = await _context.Cohorts.ToListAsync();
            StudentCohortViewModel createdStudentVM = new StudentCohortViewModel();
            createdStudentVM.cohorts = _context.Cohorts.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            createdStudentVM.cohorts.Insert(0, new SelectListItem() { Value = "0", Text = "Please Choose a Cohort" });

            //ViewData["CohortId"] = new SelectList(_context.Cohorts, "Id", "Id");
            return View(createdStudentVM);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,SlackHandle,CohortId")] Students student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<Cohorts> cohorts = await _context.Cohorts.ToListAsync();
            ViewData["CohortId"] = new SelectList(_context.Cohorts, "Id", "Id", student.CohortId);

            return View(student);
        }
        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            StudentCohortViewModel viewModel = new StudentCohortViewModel();

            // Get a single student from the database where the students ID matches the ID in the browser
            viewModel.student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);

            //Get all of the Cohorts from the database and select each cohort and turn it into a selectlistitem and then change the return into a list
            viewModel.cohorts = _context.Cohorts.Select(c => new SelectListItem
            {
                //The value has to be a string because values in HTML are all strings
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            //This is a benefit of a view model.  We were able to add a default option in the cohorts dropdown.
            //Create a select list item with a value of 0 and insert it at position 0.
            viewModel.cohorts.Insert(0, new SelectListItem() { Value = "0", Text = "Please Choose a Cohort" });


            return View(viewModel);
        }




        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,SlackHandle,CohortId")] Students student)
        {

            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(student.Id))
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

            return View(student);
        }



        // GET: Students/Delete/5
        async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Cohort)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = await _context.Students.FindAsync(id);
            _context.Students.Remove(students);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
