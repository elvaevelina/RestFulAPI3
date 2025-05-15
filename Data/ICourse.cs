using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public interface ICourse
    {
        // IEnumerable<ViewCourseWithCategory> GetCourses();
        // ViewCourseWithCategory GetCourseById(int courseId);
        Course AddCourse(Course course);
        Course UpdateCourse(Course course);
        void DeleteCourse(int courseId);
        IEnumerable<Course> GetCoursesByCategoryId(int categoryId);

        Course GetCourseByIdCourse(int courseId);
        IEnumerable<Course> GetAllCourses();
    }
}