using System;

namespace SimpleRESTApi.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public IEnumerable<Course> Courses { get; set; } = new List<Course>();

}
