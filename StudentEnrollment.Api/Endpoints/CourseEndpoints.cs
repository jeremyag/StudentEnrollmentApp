using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using StudentEnrollment.Api.Dtos.Course;
using AutoMapper;
namespace StudentEnrollment.Api.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course").WithTags(nameof(Course));

        group.MapGet("/", async (StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var courses = await db.Courses.ToListAsync();
            return mapper.Map<List<CourseDto>>(courses);
        })
        .WithName("GetAllCourses")
        .WithOpenApi()
        .Produces<List<CourseDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (int id, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            return await db.Courses.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Course model
                    ? Results.Ok(mapper.Map<CourseDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetCourseById")
        .WithOpenApi()
        .Produces<CourseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async  (int id, CourseDto courseDto, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            var foundModel = await db.Courses.FindAsync(id);
            if(foundModel is null)
            {
                return Results.NotFound();
            }

            mapper.Map(courseDto, foundModel);
            await db.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithName("UpdateCourse")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (CreateCourseDto courseDto, StudentEnrollmentDbContext db, IMapper mapper) =>
        {
            Course course = mapper.Map<Course>(courseDto);
            db.Courses.Add(course);
            await db.SaveChangesAsync();

            var resultCourseDto = mapper.Map<CourseDto>(course);
            return Results.Created($"/api/Course/{course.Id}", resultCourseDto);
        })
        .WithName("CreateCourse")
        .WithOpenApi()
        .Produces<CourseDto>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async  (int id, StudentEnrollmentDbContext db) =>
        {
            var affected = await db.Courses
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteCourse")
        .WithOpenApi()
        .Produces<CourseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
