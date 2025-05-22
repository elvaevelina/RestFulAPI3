using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.DTO
{
    public class CourseAddDTO
    {
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; }
        public double Duration { get; set; }

        public int CategoryId { get; set; }
        public CategoryDTO? Category { get; set; } // Relasi ke Category

        public int InstructorId { get; set; } // Relasi ke Instructor
        public InstructorDTO? Instructor { get; set; } = null!; // Relasi ke Instructor

    }
}