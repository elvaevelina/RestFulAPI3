using System;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data;

public interface IInstructor
{
    IEnumerable<Instructor> GetInstructors();
    Instructor GetInstructorById(int instructorId);
    Instructor AddInstructor(Instructor instructor);
    Instructor UpdateInstructor(Instructor instructor);
    void DeleteInstructor(int instructorId);

    IEnumerable<Course> GetAllCourses(); // semua course
    IEnumerable<Course> GetCoursesByInstructorId(int instructorId); // course milik 1 instructor
    Course GetCourseByInstructor(int instructorId, int courseId); // course tertentu milik instructor tertentu

    // IEnumerable<Instructor> GetInstructorsByCourseId(int courseId);
    // IEnumerable<Instructor> GetAllInstructors();
}
