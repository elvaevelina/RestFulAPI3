using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRESTApi.Models
{
    public class ViewCourseWithCategory
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; }
        public double Duration {get; set;}
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

    }
}