//using EnglishApp.ApplicationCore.Enums;
//using EnglishApp.BusinessLogic.DTOs;
//using EnglishApp.BusinessLogic.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace EnglishApp.MVC.ViewComponents
//{
//    public class CreateComment : ViewComponent
//    {
//        private readonly ICommentsService _commentsService;
//        public CreateComment(ICommentsService commentsService)
//        {
//            _commentsService = commentsService;
//        }
//        public async Task<IViewComponentResult> InvokeAsync(CourseCommentDTO courseCommentDTO,int courseId)
//        {
//            //if (ModelState.IsValid)
//            //{
//            //    var commentStatus = await _commentsService.Create(courseCommentDTO, courseId);
//            //    if(commentStatus.Code == StatusCodeEnum.Success)
//            //    {
//            //        return View("Default", courseId); // will render Views/Shared/Components/CreateComment/Default.cshtml

//            //    }

//            //}

//            return Content("Invalid model state.");

//        }
//    }
//}
