using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.DTO
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; }
        public double Duration { get; set; }
        public CategoryDTO? Category { get; set; } // Relasi ke Category
        public InstructorDTO? Instructor { get; set; } // Relasi ke Instructor
    }
}