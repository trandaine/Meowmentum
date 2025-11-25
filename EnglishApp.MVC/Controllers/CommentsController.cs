using EnglishApp.ApplicationCore.Entities;
using EnglishApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.MVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly EnglishAppDbContext _context;

        public CommentsController(EnglishAppDbContext context)
        {
            _context = context;
        }

        

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseComment = await _context.CourseComments
                .Include(c => c.Course)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseComment == null)
            {
                return NotFound();
            }

            return View(courseComment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rating,CommentText,IsReported,DateCreated,DateUpdated,CourseId,CustomerId")] CourseComment courseComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseComment.CourseId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Email", courseComment.CustomerId);
            return View(courseComment);
        }
       


    }
}
