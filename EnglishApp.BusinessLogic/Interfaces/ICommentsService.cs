using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.DTOs;

namespace EnglishApp.BusinessLogic.Interfaces
{
    public interface ICommentsService
    {
        Task<CourseCommentDTO[]> GetCourseCommentDtoById(int idCourse);
        Task<StatusCode> Create(CourseCommentDTO courseCommentDTO);
    }
}
