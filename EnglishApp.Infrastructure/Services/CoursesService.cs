using EnglishApp.ApplicationCore.Entities;
using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.DTOs;
using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Infrastructure.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly EnglishAppDbContext _context;


        public CoursesService(
            EnglishAppDbContext context
            )
        {
            _context = context;
        }

        public async Task<StatusCode> Create(CourseDTO courseModel)
        {
            var statusCode = new StatusCode();
            try
            {
                var countCourses = await _context.Courses.CountAsync();
                var newCourse = new Course
                {
                    Name = courseModel.Name.Trim(),
                    Description = courseModel.Description?.Trim(),
                    Level = courseModel.Level,
                    Thumbnail = courseModel.Thumbnail?.Trim(),
                    Price = courseModel.Price,
                    DateCreated = DateTime.Now,
                    Position = countCourses + 1
                };
                _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();
                statusCode.SetSuccess("Tạo khóa học thành công");
                return statusCode;
            }
            catch
            {
                statusCode.SetInternalError("Tạo khóa học thất bại");
            }
            return null!;
        }

        public async Task<StatusCode> Update(CourseDTO courseModel)
        {
            var statusCode = new StatusCode();

            try
            {
                var selectCourse = await _context.Courses.SingleOrDefaultAsync(c => c.Id == courseModel.Id);
                if (selectCourse != null)
                {
                    selectCourse.Name = courseModel.Name.Trim();
                    selectCourse.Description = courseModel.Description?.Trim();
                    selectCourse.Level = courseModel.Level;
                    selectCourse.Thumbnail = courseModel.Thumbnail;
                    selectCourse.Price = courseModel.Price;
                    selectCourse.DateUpdated = DateTime.Now;

                    _context.Courses.Update(selectCourse);
                    await _context.SaveChangesAsync();
                    statusCode.SetSuccess("Update khóa học thành công");
                    return statusCode;
                }
                
            }
            catch 
            {
                statusCode.SetInternalError("Tạo khóa học thất bại");


            }
            return null!;
        }


        public async Task<StatusCode> Delete(int courseId)
        {
            var statusCode = new StatusCode();
            try
            {
                var course = await _context.Courses.SingleOrDefaultAsync(c => c.Id == courseId);
                if (course != null)
                {
                    _context.Courses.Remove(course);
                }

                await _context.SaveChangesAsync();
                statusCode.SetSuccess("Xóa khóa học thành công");
                return statusCode;
            }
            catch 
            {
                statusCode.SetInternalError("Xóa khóa học thất bại");
            }
            return null!;
        }



        public async Task<CourseDTO?> GetCourseDtoById(int idCourse)
        {
            var courseDto = await _context.Courses
                .Where(c => c.Id.Equals(idCourse))
                //.Where(c => c.Id == courseId)
                .Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Level = c.Level,
                    Thumbnail = c.Thumbnail,
                    Price = c.Price,
                    DateCreated = c.DateCreated,
                    DateUpdated = c.DateUpdated,
                }).SingleOrDefaultAsync();

            return courseDto;
        }


        public async Task<CourseDTO[]> GetAllCourseDto()
        {
            var courseDtos = await _context.Courses
                .Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Level = c.Level,
                    Thumbnail = c.Thumbnail,
                    Price = c.Price,
                    DateCreated = c.DateCreated,
                    DateUpdated = c.DateUpdated,
                }).ToArrayAsync();
            return courseDtos;
        }

    }
}
