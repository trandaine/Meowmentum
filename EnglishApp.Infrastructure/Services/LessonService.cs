using EnglishApp.ApplicationCore.Entities;
using EnglishApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishApp.Infrastructure.Services
{
    public class LessonService
    {
        private readonly EnglishAppDbContext _context;

        public LessonService(EnglishAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lesson> GetAllLessons()
        {
            return _context.Lessons
                .OrderByDescending(l => l.Position) // Optional: sort by date
                .ToList();
        }


        public IEnumerable<Lesson> GetLessonsByCourseId(int courseId)
        {
            return _context.Lessons
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.Position)
                .ToList();
        }
    }
}
