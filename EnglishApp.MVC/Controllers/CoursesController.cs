using EnglishApp.ApplicationCore.Enums;
using EnglishApp.BusinessLogic.DTOs;
using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure.Constants;
using EnglishApp.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnglishApp.MVC.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        //private readonly EnglishAppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICoursesService _coursesService;


        public CoursesController(
            //EnglishAppDbContext context,
            IWebHostEnvironment env,
            ICoursesService coursesService
            )
        {
            //_context = context;
            _env = env;
            _coursesService = coursesService;
        }


        /// <summary>
        /// Reload Lesson List View Component after Create/Edit Course (21/10/2025)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult LessonLists(string filter)
        {
            return ViewComponent("LessonList", new { filter });
        }



        // GET: Courses
        // Admin page showing all courses
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Courses.ToListAsync());
            return View(await _coursesService.GetAllCourseDto());
        }

        // GET: Courses/Details/5
        // Admin page showing course details
        public async Task<IActionResult> Details(int id)
        {
            var course = await _coursesService.GetCourseDtoById(id);

            return View(course);
        }


        // GET: Courses/Create
        public IActionResult Create()
        {
            // Binding the enums to the dropdown list
            var courseLevels = Enum.GetValues(typeof(LevelEnum))
                .Cast<LevelEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.CourseLevels = new SelectList(courseLevels, "Value", "Text");
            return PartialView(nameof(Create));
        }



        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseDTO courseModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mediaHelper = new MediaHelper(_env.WebRootPath);
                    var courseThumbnailStatusCode = await mediaHelper.SaveMedia(courseModel.ImageFile, AppConstants.COURSES_FILE_PATH);
                    if (courseThumbnailStatusCode.Code == StatusCodeEnum.Success)
                    {
                        courseModel.Thumbnail = courseThumbnailStatusCode.StringReturn;
                        await _coursesService.Create(courseModel);
                    }
                    else
                    {
                        await _coursesService.Create(courseModel);
                    }
                    //=== Lưu thành công thì quay về trang Index ===//
                    //return RedirectToAction(nameof(Index));
                    return Json(new {success = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Course create new error: {ex.Message}");

                    // Return a friendly error response
                    //return BadRequest(new
                    //{
                    //    Error = "Something went wrong",
                    //    Details = ex.Message
                    //});
                }

            }
            // Binding the enums to the dropdown list if ModelState is invalid (User made a mistake)
            var courseLevels = Enum.GetValues(typeof(LevelEnum))
                .Cast<LevelEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.CourseLevels = new SelectList(courseLevels, "Value", "Text");
            return PartialView(nameof(Create), courseModel);
            //return PartialView("Create", courseModel);


        }




        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var courseDto = await _coursesService.GetCourseDtoById(id);

            // Binding the enums to the dropdown list
            var courseLevels = Enum.GetValues(typeof(LevelEnum))
                    .Cast<LevelEnum>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString()
                    }).ToList();
            ViewBag.CourseLevels = new SelectList(courseLevels, "Value", "Text", (int)courseDto.Level);
            return PartialView(nameof(Create), courseDto);
        }


        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseDTO courseModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var mediaHelper = new MediaHelper(_env.WebRootPath);
                    var courseThumbnailStatusCode = await mediaHelper.SaveMedia(courseModel.ImageFile, AppConstants.COURSES_FILE_PATH);
                    if (courseThumbnailStatusCode.Code == StatusCodeEnum.Success)
                    {
                        courseModel.Thumbnail = courseThumbnailStatusCode.StringReturn;
                        await _coursesService.Update(courseModel);
                    }
                    else
                    {
                        await _coursesService.Update(courseModel);
                    }

                    //return RedirectToAction(nameof(Index));
                    return Json(new { success = true });

                }
                catch
                {
                    TempData["Message"] = "Cập nhật thất bại";
                }
            }
            // Binding the enums to the dropdown list if ModelState is invalid (User made a mistake)
            var courseLevels = Enum.GetValues(typeof(LevelEnum))
                .Cast<LevelEnum>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                }).ToList();
            ViewBag.CourseLevels = new SelectList(courseLevels, "Value", "Text");
            return PartialView(nameof(Create), courseModel);
        }





        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _coursesService.GetCourseDtoById(id);

            return PartialView(course);
        }


        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _coursesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
