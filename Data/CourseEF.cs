using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CourseEF : ICourse
    {
        private readonly ApplicationDbContext _context;

        public CourseEF(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new course
        public Course AddCourse(Course course)
        {
            try
            {
                if(course == null)
                {
                    throw new ArgumentNullException(nameof(course), "Course cannot be null");
                }   
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course;
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception("Error adding course", dbex);
            }  
            catch(System.Exception ex)
            {
                throw new Exception("Error adding course", ex);
            }

            // catch (Exception ex)
            // {
            //     throw new Exception("Error adding course", ex);
            // }
        }

        public void DeleteCourse(int courseId)
        {
            var course = GetCourseEntityById(courseId); // Get the course entity
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            try
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting course", ex);
            }
        }

        public Course UpdateCourse(Course course)
        {
            var existingCourse = _context.Courses.FirstOrDefault(c => c.CourseId == course.CourseId); // Get the course entity
            if (existingCourse == null)
            {
                throw new Exception("Course not found");
            }

            try
            {
                // Update properties
                existingCourse.CourseName = course.CourseName;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.Duration = course.Duration;
                existingCourse.CategoryId = course.CategoryId;
                existingCourse.InstructorId = course.InstructorId;

                _context.SaveChanges();
                return existingCourse;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating course", ex);
            }
        }

        public Course GetCourseEntityById(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            return course;
        }

        public IEnumerable<Course> GetCoursesByCategoryId(int categoryId)
        {
            var courses = from c in _context.Courses.Include(c => c.Category)
                          where c.CategoryId == categoryId
                          orderby c.CourseId descending
                          select c;
            return courses;
        
        }

        public IEnumerable<Course> GetAllCourses()
        {
            var courses = _context.Courses
                    .Include(c => c.Category)
                    .Include(c => c.Instructor) // Tambahkan ini
                    .OrderByDescending(c => c.CourseId)
                    .ToList();
                return courses;
        }

        public Course GetCourseByIdCourse(int courseId)
        {
            var course = _context.Courses
            .Include(c => c.Category)
            .Include(c => c.Instructor)
            .FirstOrDefault(c => c.CourseId == courseId);

            return course;
            
        
        }
                // Get a single course with category details by ID
        // public ViewCourseWithCategory GetCourseById(int courseId)
        // {
        //     var courseWithCategory = _context.Courses
        //         .Where(c => c.CourseId == courseId)
        //         .Select(c => new ViewCourseWithCategory
        //         {
        //             CourseId = c.CourseId,
        //             CourseName = c.CourseName,
        //             CourseDescription = c.CourseDescription,
        //             Duration = c.Duration,
        //             CategoryId = c.CategoryId,
        //             CategoryName = c.Category.CategoryName // Assuming Category is properly related
        //         })
        //         .FirstOrDefault();

        //     if (courseWithCategory == null)
        //     {
        //         throw new Exception("Course not found");
        //     }
        //     return courseWithCategory;
        // }

        // Get all courses with category details
        // public IEnumerable<ViewCourseWithCategory> GetCourses()
        // {
        //     return _context.Courses
        //         .Select(c => new ViewCourseWithCategory
        //         {
        //             CourseId = c.CourseId,
        //             CourseName = c.CourseName,
        //             CourseDescription = c.CourseDescription,
        //             Duration = c.Duration,
        //             CategoryId = c.CategoryId,
        //             CategoryName = c.Category.CategoryName // Assuming Category is properly related
        //         })
        //         .ToList();
        // }
    }
}