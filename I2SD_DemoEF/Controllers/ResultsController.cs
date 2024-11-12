using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using I2SD_DemoEF;
using I2SD_DemoEF.Data;

namespace I2SD_DemoEF.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Results.Include(r => r.Course).Include(r => r.Student).Include(r => r.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Course)
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = 
                new SelectList(_context.Courses.OrderBy(c=>c.CourseName), "CourseID", "CourseName");
            ViewData["StudentID"] = 
                new SelectList(
                _context.Students.ToList().OrderBy(s => s.ToString())
                    .Select(s => new { s.StudentID, Name = s.ToString() }),
                "StudentID",
                "Name"
            );
            ViewData["TeacherID"] = 
                new SelectList(
                _context.Teachers.ToList().OrderBy(t => t.ToString())
                    .Select(s => new { s.TeacherID, Name = s.ToString() }),
                "TeacherID",
                "Name"
            );
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,CourseID,TeacherID,Score,ResultDate")] Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] =
                new SelectList(_context.Courses.OrderBy(c => c.CourseName), "CourseID", "CourseName");
            ViewData["StudentID"] =
                new SelectList(
                _context.Students.ToList().OrderBy(s => s.ToString())
                    .Select(s => new { s.StudentID, Name = s.ToString() }),
                "StudentID",
                "Name"
            );
            ViewData["TeacherID"] =
                new SelectList(
                _context.Teachers.ToList().OrderBy(t => t.ToString())
                    .Select(s => new { s.TeacherID, Name = s.ToString() }),
                "TeacherID",
                "Name"
            );
            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Courses.OrderBy(c => c.CourseName), "CourseID", "CourseName");
            ViewData["StudentID"] = new SelectList(
                _context.Students.ToList().OrderBy(s => s.ToString())
                    .Select(s => new { s.StudentID, Name = s.ToString() }),
                "StudentID",
                "Name"
            );
            ViewData["TeacherID"] = new SelectList(
                _context.Teachers.ToList().OrderBy(t => t.ToString())
                    .Select(s => new { s.TeacherID, Name = s.ToString() }),
                "TeacherID",
                "Name"
            );
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentID,CourseID,TeacherID,Score,ResultDate")] Result result)
        {
            if (id != result.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.StudentID))
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
            ViewData["CourseID"] =
                new SelectList(_context.Courses.OrderBy(c => c.CourseName), "CourseID", "CourseName");
            ViewData["StudentID"] =
                new SelectList(
                _context.Students.ToList().OrderBy(s => s.ToString())
                    .Select(s => new { s.StudentID, Name = s.ToString() }),
                "StudentID",
                "Name"
            );
            ViewData["TeacherID"] =
                new SelectList(
                _context.Teachers.ToList().OrderBy(t => t.ToString())
                    .Select(s => new { s.TeacherID, Name = s.ToString() }),
                "TeacherID",
                "Name"
            );
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Course)
                .Include(r => r.Student)
                .Include(r => r.Teacher)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _context.Results.FindAsync(id);
            if (result != null)
            {
                _context.Results.Remove(result);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(string id)
        {
            return _context.Results.Any(e => e.StudentID == id);
        }
    }
}
