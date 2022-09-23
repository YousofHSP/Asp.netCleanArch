using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private UniversityDBContext _ctx;
        public CourseRepository(UniversityDBContext ctx)
        {
            _ctx = ctx;
        }

        public Course GetCourse(int courseId)
        {
            return _ctx.Courses.Find(courseId);
        }

        public IEnumerable<Course> GetCourses()
        {
            return  _ctx.Courses;
        }
    }
}
