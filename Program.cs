using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// add ef core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Dependency injection 
builder.Services.AddScoped<Icategory, CategoryEF>();
builder.Services.AddScoped<IInstructor, InstructorEF>();
builder.Services.AddScoped<ICourse, CourseEF>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// app.MapGet("api/v1/helloservices", (string? id) => 
// {
//     return $"Hello asp web API: id={id}!";
// });
// app.MapGet("api/v1/helloservices/{name}", (string name) => $"Hello {name}!");

// app.MapGet("api/v1/luas-segitiga", (double alas, double tinggi) => 
// {
//     double luas = 0.5*alas*tinggi;
//     return $"Luas segitiga dengan alas = {alas} dan tinggi = {tinggi} adalah {luas}";
// });

app.MapGet("api/v1/categories", (Icategory categoryData) =>
{
    var categories = categoryData.GetCategories();
    return categories;
    // Category category = new Category();
    // category.CategoryId = 1;
    // category.CategoryName = "ASP.NET Core";
    // return category;
});

app.MapGet("api/v1/categories/{id}", (Icategory categoryData, int id) =>
{
    var category = categoryData.GetCategoryById(id);
    return category;
});

app.MapPost("api/v1/categories", (Icategory categoryData, Category category) =>
{
    var newCategory = categoryData.AddCategory(category);
    return newCategory;
});

app.MapPut("api/v1/categories", (Icategory categoryData, Category category) =>
{
    var updatedCategory = categoryData.UpdateCategory(category);
    return updatedCategory;
});

app.MapDelete("api/v1/categories/{id}", (Icategory categoryData, int id) =>
{
    categoryData.DeleteCategory(id);
    return Results.NoContent();
});

// instructor
app.MapGet("api/v1/instructors",(IInstructor instructorData)=>
{
    var instructors = instructorData.GetInstructors();
    return instructors;
    // Instructor instructor = new Instructor();
    // instructor.InstructorId=1;
    // instructor.InstructorName="pumkin";
    // instructor.InstructorEmail="pumkin@gmail.com";
    // instructor.InstructorPhone="0811111111";
    // instructor.InstructorAddress="Merak street";
    // instructor.InstructorCity="Yogyakarta";
    // return instructor;
});

app.MapGet("api/v1/instructors/{id}", (IInstructor insturctorData, int id) =>
{
    var instructor = insturctorData.GetInstructorById(id);
    return instructor;
});

app.MapPost("api/v1/instructors", (IInstructor instructorData, Instructor instructor) =>
{
    var newInsturctor = instructorData.AddInstructor(instructor);
    return newInsturctor;
});

app.MapPut("api/v1/instructors", (IInstructor instructorData, Instructor instructor) =>
{
    var updatedInstructor = instructorData.UpdateInstructor(instructor);
    return updatedInstructor;
});

app.MapDelete("api/v1/instructors/{id}", (IInstructor instructorData, int id) =>
{
    instructorData.DeleteInstructor(id);
    return Results.NoContent();
});

app.MapGet("api/v1/courses", (ICourse courseData) =>
{
    var courses = courseData.GetCourses();
    return courses;
});

app.MapGet("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    var course = courseData.GetCourseById(id);
    return course;
});

app.MapPost("api/v1/courses", (ICourse courseData, Course course) =>
{
    var newCourse = courseData.AddCourse(course);
    return newCourse;
});

app.MapPut("api/v1/courses", (ICourse courseData, Course course) =>
{
    var updatedCourse = courseData.UpdateCourse(course);
    return updatedCourse;
});

app.MapDelete("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    courseData.DeleteCourse(id);
    return Results.NoContent();
});

app.Run();
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


