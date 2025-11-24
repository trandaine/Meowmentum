using EnglishApp.ApplicationCore.Entities;
using EnglishApp.BusinessLogic.DTOs;
using EnglishApp.ApplicationCore.Enums;
using EnglishApp.Infrastructure;
using EnglishApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.MVC.Controllers
{
    [Authorize]
    public class LessonsController : Controller
    {
        private readonly EnglishAppDbContext _context;
        private readonly LessonService _lessonService;

        public LessonsController
        (
            EnglishAppDbContext context,
            LessonService lessonService
            )
        {
            _context = context;
            _lessonService = lessonService;
        }

        public async Task<IActionResult> LessonComponentList(int courseId)
        {
            var lessons = await _context.Lessons
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.Position)
                .ToListAsync();

            // Pass the courseId to the view using ViewData
            ViewData["CourseId"] = courseId;

            // Replace the erroneous line with the correct usage of ViewComponent
            // You need to specify the name of the ViewComponent as a string and pass the lessons as an argument
            return ViewComponent("LessonList", new { lessons = lessons, courseId = courseId });
        }



        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var englishAppDbContext = _context.Lessons.Include(l => l.Course);
            return View(await englishAppDbContext.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create(int? courseId)
        {
            // Binding the courses to the dropdown list
            // Binding the enums to the dropdown list
            var contentTypes = Enum.GetValues(typeof(ContentTypeEnum))
                .Cast<ContentTypeEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.LessonContentType = new SelectList(contentTypes, "Value", "Text");
            //ViewBag.CourseList = new SelectList(_context.Courses.ToList(), "Id", "Name");

            // Initialize a new LessonDTO model with the provided courseId
            var model = new LessonDTO();
            if (courseId.HasValue)
            {
                model.CourseId = courseId.Value;
            }
            ;
            return PartialView(model);


        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] LessonDTO lessonModel)
        {
            if (!ModelState.IsValid)
                return PartialView(lessonModel);
            //return Json(new { isOkay = false, errors = ModelState });
            try
            {
                var countLessons = await _context.Lessons.CountAsync();


                var newLesson = new Lesson
                {
                    Name = lessonModel.Name.Trim(),
                    Content = lessonModel.Content?.Trim(),
                    ContentType = lessonModel.ContentType,
                    Video = lessonModel.Video?.Trim(),
                    Audio = lessonModel.Audio?.Trim(),
                    DateCreated = DateTime.Now,
                    Position = countLessons + 1,
                    //CourseId = lessonModel.CourseId,
                    CourseId = lessonModel.CourseId,
                };
                _context.Add(newLesson);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Create));
                return Json(new { isOkay = true });
            }
            catch
            {

                //return View(nameof(Create), lessonModel);
                return PartialView(nameof(Create), lessonModel);
                //return Json(new { isOkay = true });
            }
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonDto = await _context.Lessons
                .Where(c => c.Id.Equals(id))
                .Select(c => new LessonDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Content = c.Content,
                    ContentType = c.ContentType,
                    Video = c.Video,
                    Audio = c.Audio,
                    CourseId = c.CourseId
                }).SingleOrDefaultAsync();
            if (lessonDto == null)
            {
                return NotFound();
            }
            var contentTypes = Enum.GetValues(typeof(ContentTypeEnum))
                .Cast<ContentTypeEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.LessonContentType = new SelectList(contentTypes, "Value", "Text");
            ViewBag.CourseList = new SelectList(_context.Courses.ToList(), "Id", "Name", lessonDto.CourseId);
            return PartialView(nameof(Edit), lessonDto);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] LessonDTO lessonModel)
        {
            if (!ModelState.IsValid)
                return PartialView(lessonModel);
            try
            {
                var selectLesson = await _context.Lessons.FindAsync(lessonModel.Id);
                if (selectLesson == null)
                {
                    return BadRequest();
                }

                selectLesson.Name = lessonModel.Name.Trim();
                selectLesson.Name = lessonModel.Name.Trim();
                selectLesson.Content = lessonModel.Content?.Trim();
                selectLesson.ContentType = lessonModel.ContentType;
                selectLesson.Video = lessonModel.Video?.Trim();
                selectLesson.Audio = lessonModel.Audio?.Trim();
                selectLesson.DateUpdated = DateTime.Now;
                selectLesson.CourseId = lessonModel.CourseId;

                _context.Lessons.Update(selectLesson);
                await _context.SaveChangesAsync();
                //ViewBag.Message = "Cập nhật thành công";
                TempData["Message"] = "Cập nhật thành công";
                return Json(new { isOkay = true });

                //return RedirectToAction(nameof(Create));
            }
            catch
            {
                TempData["Message"] = "Cập nhật thất bại";
                return PartialView(nameof(Edit), lessonModel);

                //return RedirectToAction(nameof(Create));
            }
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return PartialView(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
