using EnglishApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.MVC.ViewComponents
{
    public class LessonList : ViewComponent
    {
        private readonly EnglishAppDbContext _context;
        public LessonList(EnglishAppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int courseId)
        {
            var lessons = await _context.Lessons
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.Position)
                //.Select(l => new LessonViewModel
                //{
                //    Id = l.Id,
                //    Title = l.Title,
                //    Description = l.Description,
                //    Duration = l.Duration,
                //})
                .ToListAsync();

            // Pass the courseId to the view using ViewData
            ViewData["CourseId"] = courseId;

            return View(lessons);
            //var lessons = await _context.Lessons
            //    .OrderBy(c => c.Position)
            //    .ToListAsync();

            //return View(lessons);
        }
    }
}
