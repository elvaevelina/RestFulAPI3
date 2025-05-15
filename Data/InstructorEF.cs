using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class InstructorEF : IInstructor
    {
        // ef context 
        private readonly ApplicationDbContext _context;
        public InstructorEF(ApplicationDbContext context)
        {
            _context = context;
        }


        public Instructor AddInstructor(Instructor instructor)
        {
            try
            {
                if(instructor == null)
                {
                    throw new ArgumentNullException(nameof(instructor), "Instructor cannot be null");
                }
                _context.Instructors.Add(instructor);
                _context.SaveChanges();
                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding instructor", ex);

            }
        }

        public void DeleteInstructor(int instructorId)
        {
            var instructor = GetInstructorById(instructorId);
            if(instructor == null)
            {
                throw new Exception("Instructor not found");
            }
            try
            {
                _context.Instructors.Remove(instructor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting instructor", ex);
            }
        }

        public IEnumerable<Instructor> GetAllInstructors()
        {
            var instructors = from i in _context.Instructors.Include(i => i.Courses)
                             orderby i.InstructorId descending
                             select i;
            return instructors;
        }

        public Instructor GetInstructorById(int instructorId)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.InstructorId == instructorId);
            if (instructor == null)
            {
                throw new Exception("Instructor not found");
            }
            return instructor;
        }

        public IEnumerable<Instructor> GetInstructors()
        {
            throw new NotImplementedException();
        }

        // public IEnumerable<Instructor> GetInstructors()
        // {
        //     var instructors = _context.Instructors.OrderByDescending(i => i.InstructorId).ToList();
        //     return instructors;
        // }

        public IEnumerable<Instructor> GetInstructorsByCourseId(int courseId)
        {
            var instructors = from i in _context.Instructors.Include(i => i.Courses)
                              where i.Courses.Any(c => c.CourseId == courseId)
                              orderby i.InstructorId descending
                              select i;
            return instructors;
        }
        
        public Instructor UpdateInstructor(Instructor instructor)
        {
            var existingInstructor = GetInstructorById(instructor.InstructorId);
            if (existingInstructor == null)
            {
                throw new Exception("Instructor not found");
            }
            try
            {
                existingInstructor.InstructorName = instructor.InstructorName;
                existingInstructor.InstructorEmail = instructor.InstructorEmail;
                existingInstructor.InstructorPhone = instructor.InstructorPhone;
                existingInstructor.InstructorAddress = instructor.InstructorAddress;
                existingInstructor.InstructorCity = instructor.InstructorCity;

                _context.Instructors.Update(existingInstructor); // Tambahkan baris ini
                _context.SaveChanges();

                return existingInstructor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating instructor", ex);
            }
        }
    }
}