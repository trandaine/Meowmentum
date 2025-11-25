using EnglishApp.ApplicationCore.Entities;
using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.DTOs;
using EnglishApp.BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnglishApp.Infrastructure.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly EnglishAppDbContext _context;
        public CommentsService(EnglishAppDbContext context)
        {
            _context = context;
        }

        public async Task<StatusCode> Create(CourseCommentDTO courseCommentDTO)
        {
            var statusCode = new StatusCode();  
            try
            {
                var dateNow = DateTime.UtcNow;
                var courseComment = new CourseComment
                {
                    //Rating = courseCommentDTO.Rating,
                    CommentText = courseCommentDTO.CommentText,
                    //IsReported = courseCommentDTO.IsReported,
                    DateCreated = dateNow,
                    Subject = courseCommentDTO.Subject,
                    CourseId = courseCommentDTO.CourseId,
                    CustomerId = courseCommentDTO.CustomerId
                };
                _context.CourseComments.Add(courseComment);
                await _context.SaveChangesAsync();
                statusCode.SetSuccess("Comment created successfully.");
                return statusCode;
            }
            catch
            {
                statusCode.SetInternalError("An error occurred while creating the comment.");
            }
            return null!;
        }

        public async Task<CourseCommentDTO[]> GetCourseCommentDtoById(int idCourse)
        {
            var courseComment = await _context.CourseComments
                .Where(c => c.CourseId == idCourse)
                .Select(c => new CourseCommentDTO
                {
                    Id = c.Id,
                    Rating = c.Rating,
                    CommentText = c.CommentText,
                    Subject = c.Subject,
                    IsReported = c.IsReported,
                    DateCreated = c.DateCreated,
                    DateUpdated = c.DateUpdated,
                    CourseId = c.CourseId,
                    CustomerId = c.CustomerId,
                    CustomerName = _context.Customers
                    .Where(cus => cus.Id.Equals(c.CustomerId))
                    .Select(cus => cus.Name)
                    .FirstOrDefault()
                })
                .ToArrayAsync();
            return courseComment;
        }

    }
}


