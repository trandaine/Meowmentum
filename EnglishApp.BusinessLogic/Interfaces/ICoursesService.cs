using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.DTOs;

namespace EnglishApp.BusinessLogic.Interfaces
{
    public interface ICoursesService
    {
        Task<CourseDTO?> GetCourseDtoById(int idCourse);
        Task<CourseDTO[]> GetAllCourseDto();
        Task<StatusCode> Create(CourseDTO courseModel);
        Task<StatusCode> Update(CourseDTO courseModel);
        Task<StatusCode> Delete(int courseId);

    }
}
