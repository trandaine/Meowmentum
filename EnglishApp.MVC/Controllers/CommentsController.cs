using EnglishApp.ApplicationCore.Enums;
using EnglishApp.BusinessLogic.DTOs;
using EnglishApp.BusinessLogic.Interfaces;
using EnglishApp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.MVC.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly EnglishAppDbContext _context;
        private readonly ICommentsService _commentsService;
        private readonly ICustomerService _customerService;

        public CommentsController(
            EnglishAppDbContext context,
            ICommentsService commentsService,
            ICustomerService customerService
            )
        {
            _commentsService = commentsService;
            _customerService = customerService;
            _context = context;
        }



        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseComment = await _context.CourseComments
                .Include(c => c.Course)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseComment == null)
            {
                return NotFound();
            }

            return View(courseComment);
        }

        // GET: Comments/Create
        public async Task<IActionResult> Create(int courseId)
        {
            var userId = _customerService.GetCurrentUserId();
            var customerId = await _customerService.GetCustomerDtoByUserId(userId);
            var courseCommentDTO = new CourseCommentDTO
            {
                CustomerId = customerId.Id,
                CourseId = courseId
            };
            //return PartialView();
            return PartialView(courseCommentDTO);

        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCommentDTO courseCommentDTO)
        {
            var courseCommentStatus = await _commentsService.Create(courseCommentDTO);
            if (courseCommentStatus.Code == StatusCodeEnum.Success)
            {
                return Json(new { success = true });
            }
            return PartialView(nameof(Create), courseCommentDTO);
        }

    }
}
