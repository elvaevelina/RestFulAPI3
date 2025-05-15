using System;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data;

public interface IInstructor
{
    IEnumerable<Instructor>GetInstructors();
    Instructor GetInstructorById(int instructorId);
    Instructor AddInstructor(Instructor instructor);
    Instructor UpdateInstructor(Instructor instructor);
    void DeleteInstructor(int instructorId);


    IEnumerable<Instructor> GetInstructorsByCourseId(int courseId);
    IEnumerable<Instructor> GetAllInstructors();
}
