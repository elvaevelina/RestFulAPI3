using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; }
        public double Duration { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; } // Relasi ke Category

        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; } = null!; // Relasi ke Instructor
        


    }
}