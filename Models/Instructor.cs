using System;

namespace SimpleRESTApi.Models;

public class Instructor
{
    public int InstructorId { get; set; }
    public string InstructorName { get; set; } = null!;
    public string InstructorEmail { get; set; } = null!;
    public string InstructorPhone { get; set; } = null!;
    public string InstructorAddress { get; set; } = null!;
    public string InstructorCity { get; set; } = null!;
    public string InstructorCountry { get; set; } = null!;
    // public int CourseId { get; set; }
    // public IEnumerable<Course> Courses { get; set; } = new List<Course>();
    public ICollection<Course> Courses { get; set; } = new List<Course>();

}
