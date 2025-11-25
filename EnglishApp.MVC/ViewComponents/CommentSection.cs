using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnglishApp.MVC.ViewComponents
{
    public class CommentSection : ViewComponent
    {
        private readonly ICommentsService _commentsService;
        public CommentSection(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int courseId)
        {
            try
            {
                var comments = await _commentsService.GetCourseCommentDtoById(courseId);
                return View(comments); // will render Views/Shared/Components/CourseComments/Default.cshtml
            }
            catch (Exception ex)
            {
                // Log the error (or temporarily return the message for debugging)
                return Content($"Error: {ex.Message}");
            }

            
        }
    }
}
