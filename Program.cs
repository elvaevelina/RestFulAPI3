using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRESTApi.Data;
using SimpleRESTApi.DTO;
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
    var courses = courseData.GetAllCourses();
    var result = courses.Select(course => new {
        CourseId = course.CourseId,
        CourseName = course.CourseName,
        CourseDescription = course.CourseDescription,
        Duration = course.Duration,
        CategoryId = course.Category?.CategoryId ?? 0,
        CategoryName = course.Category?.CategoryName ?? "",
        InstructorId = course.Instructor?.InstructorId ?? 0,
        InstructorName = course.Instructor?.InstructorName ?? ""
    }).ToList();
    return result;
});

app.MapGet("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    var course = courseData.GetCourseByIdCourse(id);
    if (course == null)
    {
        return Results.NotFound();
    }
    var result = new
    {
        CourseId = course.CourseId,
        CourseName = course.CourseName,
        CourseDescription = course.CourseDescription,
        Duration = course.Duration,
        CategoryId = course.Category?.CategoryId ?? 0,
        CategoryName = course.Category?.CategoryName ?? "",
        InstructorId = course.Instructor?.InstructorId ?? 0,
        InstructorName = course.Instructor?.InstructorName ?? ""
    };

    return Results.Ok(result);
});

app.MapPost("api/v1/courses", (ICourse courseData, CourseAddDTO courseAddDTO) =>
{
    Course course = new Course()
    {
        CourseName = courseAddDTO.CourseName,
        CourseDescription = courseAddDTO.CourseDescription,
        Duration = courseAddDTO.Duration,
        CategoryId = courseAddDTO.CategoryId,
        InstructorId = courseAddDTO.InstructorId
    };
    try
    { 
        var newCourse = courseData.AddCourse(course);

        // Query ulang agar relasi Category & Instructor terisi
        var insertedCourse = courseData.GetCourseByIdCourse(newCourse.CourseId);

        var result = new
        {
            CourseId = insertedCourse.CourseId,
            CourseName = insertedCourse.CourseName,
            CourseDescription = insertedCourse.CourseDescription,
            Duration = insertedCourse.Duration,
            CategoryId = insertedCourse.Category?.CategoryId ?? insertedCourse.CategoryId,
            CategoryName = insertedCourse.Category?.CategoryName ?? "",
            InstructorId = insertedCourse.Instructor?.InstructorId ?? insertedCourse.InstructorId ?? 0,
            InstructorName = insertedCourse.Instructor?.InstructorName ?? ""
        };

        return Results.Created($"/api/v1/courses/{insertedCourse.CourseId}", result);
    }
    catch (DbUpdateException dbex)
    {
        throw new Exception("Error adding course", dbex);
    }  
    catch(System.Exception ex)
    {
        throw new Exception("Error adding course", ex);
    }
});

app.MapPut("api/v1/courses", (ICourse courseData, CourseUpdateDTO courseUpdateDTO) =>
{
    Course course = new Course()
    {
        CourseId = courseUpdateDTO.CourseId,
        CourseName = courseUpdateDTO.CourseName,
        CourseDescription = courseUpdateDTO.CourseDescription,
        Duration = courseUpdateDTO.Duration,
        CategoryId = courseUpdateDTO.CategoryId,
        InstructorId = courseUpdateDTO.InstructorId
    };
    try
    {
        var updatedCourse = courseData.UpdateCourse(course);

        // Query ulang agar relasi Category & Instructor terisi
        var refreshedCourse = courseData.GetCourseByIdCourse(updatedCourse.CourseId);

        var result = new
        {
            CourseId = refreshedCourse.CourseId,
            CourseName = refreshedCourse.CourseName,
            CourseDescription = refreshedCourse.CourseDescription,
            Duration = refreshedCourse.Duration,
            CategoryId = refreshedCourse.Category?.CategoryId ?? refreshedCourse.CategoryId,
            CategoryName = refreshedCourse.Category?.CategoryName ?? "",
            InstructorId = refreshedCourse.Instructor?.InstructorId ?? refreshedCourse.InstructorId ?? 0,
            InstructorName = refreshedCourse.Instructor?.InstructorName ?? ""
        };

        return Results.Ok(result);
    }
    catch(DbUpdateException dbex)
    {
        throw new Exception("Error updating course", dbex);
    }  
    catch(System.Exception ex)
    {
        throw new Exception("Error updating course", ex);
    }
});

app.MapDelete("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    try
    {
        courseData.DeleteCourse(id);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
    return Results.NoContent();
    // courseData.DeleteCourse(id);
    // return Results.NoContent();
});

app.Run();
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


