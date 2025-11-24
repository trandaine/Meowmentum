using EnglishApp.BusinessLogic.DTOs;
using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure;
using EnglishApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EnglishApp.MVC.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ICoursesService _coursesService;
        private readonly EnglishAppDbContext _context;

        public HomeController(
            //ILogger<HomeController> logger, 
            ICoursesService coursesService,
            EnglishAppDbContext context
            )
        {
            //_logger = logger;
            _coursesService = coursesService;
            _context = context;
        }


        // GET: Courses/Home
        // This is the main page showing all courses to customers
        public async Task<IActionResult> Index()
        {
            var courses = await _coursesService.GetAllCourseDto();
            return View(courses);
        }



        // GET: Home/CourseDetails/id
        // This is the course details page for customers 
        public async Task<IActionResult> CourseDetails(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            var course = await _coursesService.GetCourseDtoById(id);  
            //var course = await _context.Courses
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (course == null)
            //{
            //    return NotFound();
            //}

            return View(course);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
