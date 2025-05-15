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
                InstructorCity = "Yogyakarta",
                CourseId = 1},
                new Instructor{InstructorId = 2,
                InstructorName = "Roti",
                InstructorEmail= "roti@gmail.com",
                InstructorPhone="08222222",
                InstructorAddress="Lele street",
                InstructorCity = "Sleman",
                CourseId = 2},
                new Instructor{InstructorId = 3,
                InstructorName = "Tahu",
                InstructorEmail= "tahu@gmail.com",
                InstructorPhone="08333333",
                InstructorAddress="Mawar street",
                InstructorCity = "Bantul",
                CourseId = 3},
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
            }
            return existingInstructor;
        }

        IEnumerable<Instructor> IInstructor.GetInstructors()
        {
            return GetInstructors();
        }

        IEnumerable<Instructor> IInstructor.GetInstructorsByCourseId(int courseId)
        {
            // Assuming a CourseId property exists in the Instructor model
            return _instructor.Where(i => i.CourseId == courseId);
        }

        IEnumerable<Instructor> IInstructor.GetAllInstructors()
        {
            return GetInstructors();
        }
    }


    
    // private List<Instructor> _instructor = new List<Instructor>();

    // public InstructorDal()
    // {
    //     _instructor = new List<Instructor>
    //     {
    //         new Instructor{InstructorId = 1,
    //         InstructorName = "Pumkin",
    //         InstructorEmail= "pumkin@gmail.com",
    //         InstructorPhone="08111111",
    //         InstructorAddress="Merak street",
    //         InstructorCity = "Yogyakarta",
    //         CourseId = 1},
    //         new Instructor{InstructorId = 2,
    //         InstructorName = "Roti",
    //         InstructorEmail= "roti@gmail.com",
    //         InstructorPhone="08222222",
    //         InstructorAddress="Lele street",
    //         InstructorCity = "Sleman",
    //         CourseId = 2},
    //         new Instructor{InstructorId = 3,
    //         InstructorName = "Tahu",
    //         InstructorEmail= "tahu@gmail.com",
    //         InstructorPhone="08333333",
    //         InstructorAddress="Mawar street",
    //         InstructorCity = "Bantul",
    //         CourseId = 3},
    //     };
    // }

    // public Instructor GetInstructorById(int instructorId)
    // {
    //     var instructor = _instructor.FirstOrDefault(i => i.InstructorId == instructorId);
    //     if (instructor == null)
    //     {
    //         throw new Exception("Instructor not found");
    //     }
    //     return instructor;
    // }

    // public IEnumerable<Instructor> GetInstructors()
    // {
    //     return _instructor;
    // }

    // public Instructor AddInstructor(Instructor instructor)
    // {
    //     _instructor.Add(instructor);
    //     return instructor;
    // }

    // public void DeleteInstructor(int instructorId)
    // {
    //     var instructor = GetInstructorById(instructorId);
    //     if (instructor != null)
    //     {
    //         _instructor.Remove(instructor);
    //     }
    // }

    // public Instructor UpdateInstructor(Instructor instructor)
    // {
    //     var existingInstructor = GetInstructorById(instructor.InstructorId);
    //     if (existingInstructor != null)
    //     {
    //         existingInstructor.InstructorName = instructor.InstructorName;
    //         existingInstructor.InstructorEmail = instructor.InstructorEmail;
    //         existingInstructor.InstructorPhone = instructor.InstructorPhone;
    //         existingInstructor.InstructorAddress = instructor.InstructorAddress;
    //     }
    //     return existingInstructor;
    // }

    // IEnumerable<Instructor> IInstructor.GetInstructors()
    // {
    //     return GetInstructors();
    // }

    // IEnumerable<Instructor> IInstructor.GetInstructorsByCourseId(int courseId)
    // {
    //     // Assuming a CourseId property exists in the Instructor model
    //     return _instructor.Where(i => i.CourseId == courseId);
    // }

    // IEnumerable<Instructor> IInstructor.GetAllInstructors()
    // {
    //     return _instructor;
    // }

