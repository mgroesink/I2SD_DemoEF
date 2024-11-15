using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using I2SD_DemoEF;
using I2SD_DemoEF.Data;
using System.Globalization;

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
            var applicationDbContext = _context.Results.Include(r => r.Course).Include(r => r.Student)
                .Include(r => r.Teacher).OrderBy(r=>r.Student.LastName)
                .ThenBy(r=>r.Student.FirstName).ThenBy(r=>r.Course.CourseName).ThenBy(r=>r.ResultDate);
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
        public async Task<IActionResult> Edit(string studentID, string courseID, DateTime resultDate)
        {
            var result = await _context.Results.Include(r=>r.Course).Include(r=>r.Student).Include(r=>r.Teacher)
                .FirstOrDefaultAsync(m => m.StudentID == studentID && m.CourseID == courseID && m.ResultDate == resultDate);
            if (result == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Edit([Bind("StudentID,CourseID,TeacherID,Score")] Result oldResult, string resultDate)
        {
            var resultDt = DateTime.ParseExact(resultDate, "MM/dd/yyyy HH:mm:ss",CultureInfo.InvariantCulture);
            string sql = "UPDATE results SET TeacherID = {1}, Score = {2} WHERE StudentID = {4} AND CourseID = {5} AND ResultDate = {6}";
            _context.Database.ExecuteSqlRaw(sql, oldResult.CourseID, oldResult.TeacherID, 
                oldResult.Score, oldResult.ResultDate, oldResult.StudentID, oldResult.CourseID, resultDt);

            return RedirectToAction("Index");
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
