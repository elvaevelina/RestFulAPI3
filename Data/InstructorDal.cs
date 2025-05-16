using System;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data;

public class InstructorDal : IInstructor
{
    private List<Instructor> _instructor = new List<Instructor>();

    public InstructorDal()
    {
        _instructor = new List<Instructor>
        {
            new Instructor{InstructorId = 1,
            InstructorName = "Pumkin",
            InstructorEmail= "pumkin@gmail.com",
            InstructorPhone="08111111",
            InstructorAddress="Merak street",
            InstructorCity = "Yogyakarta"
            },
            new Instructor{InstructorId = 2,
            InstructorName = "Roti",
            InstructorEmail= "roti@gmail.com",
            InstructorPhone="08222222",
            InstructorAddress="Lele street",
            InstructorCity = "Sleman"
            },
            new Instructor{InstructorId = 3,
            InstructorName = "Tahu",
            InstructorEmail= "tahu@gmail.com",
            InstructorPhone="08333333",
            InstructorAddress="Mawar street",
            InstructorCity = "Bantul"
            },
        };
    }

    public Instructor GetInstructorById(int instructorId)
    {
        var instructor = _instructor.FirstOrDefault(i => i.InstructorId == instructorId);
        if (instructor == null)
        {
            throw new Exception("Instructor not found");
        }
        return instructor;
    }

    public IEnumerable<Instructor> GetInstructors()
    {
        return _instructor;
    }

    public Instructor AddInstructor(Instructor instructor)
    {
        _instructor.Add(instructor);
        return instructor;
    }

    public void DeleteInstructor(int instructorId)
    {
        var instructor = GetInstructorById(instructorId);
        if (instructor != null)
        {
            _instructor.Remove(instructor);
        }
    }

    public Instructor UpdateInstructor(Instructor instructor)
    {
        var existingInstructor = GetInstructorById(instructor.InstructorId);
        if (existingInstructor != null)
        {
            existingInstructor.InstructorName = instructor.InstructorName;
            existingInstructor.InstructorEmail = instructor.InstructorEmail;
            existingInstructor.InstructorPhone = instructor.InstructorPhone;
            existingInstructor.InstructorAddress = instructor.InstructorAddress;
            existingInstructor.InstructorCity = instructor.InstructorCity; // Added this line
            // existingInstructor.CourseId = instructor.CourseId;           // Added this line

        }
        return existingInstructor;
    }

    IEnumerable<Instructor> IInstructor.GetInstructors()
    {
        return GetInstructors();
    }

    public IEnumerable<Course> GetAllCourses()
    {
        return _instructor.Select(i => new Course
        {
            // CourseId = i.CourseId,
            CourseName = i.InstructorName,
            CourseDescription = i.InstructorEmail,
            InstructorId = i.InstructorId
        });
    }

    public IEnumerable<Course> GetCoursesByInstructorId(int instructorId)
    {
        return _instructor
            .Where(i => i.InstructorId == instructorId)
            .Select(i => new Course
            {
                // CourseId = i.CourseId,
                CourseName = i.InstructorName,
                CourseDescription = i.InstructorEmail,
                InstructorId = i.InstructorId
            });
    }

    

    public Course GetCourseByInstructor(int instructorId, int courseId)
    {
        // Since Instructor does not have CourseId, we cannot filter by courseId.
        // Instead, return a dummy Course or throw an exception if not found.
        var instructor = _instructor.FirstOrDefault(i => i.InstructorId == instructorId);
        if (instructor == null)
        {
            throw new Exception("Instructor not found");
        }
        // Return a Course object with dummy data or based on instructor info
        return new Course
        {
            CourseId = courseId,
            CourseName = instructor.InstructorName,
            CourseDescription = instructor.InstructorEmail,
            InstructorId = instructor.InstructorId
        };
    }
}
