using EnglishApp.BusinessLogic.DTOs;
using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;
        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }


        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> Get()
        {
            var courses = await _coursesService.GetAllCourseDto();
            return Ok(courses);
        }



        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> Get(int id)
        {
            var course = await _coursesService.GetCourseDtoById(id);
            return Ok(course);
        }



        // POST api/<CoursesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }



        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }



        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
