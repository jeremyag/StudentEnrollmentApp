using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using StudentEnrollment.Api.Dtos.Course;
using AutoMapper;
using StudentEnrollment.Data.Contracts;
namespace StudentEnrollment.Api.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course").WithTags(nameof(Course));

        group.MapGet("/", async (ICourseRepository repo, IMapper mapper) =>
        {
            var courses = await repo.GetAllAsync();
            return mapper.Map<List<CourseDto>>(courses);
        })
        .WithName("GetAllCourses")
        .WithOpenApi()
        .Produces<List<CourseDto>>(StatusCodes.Status200OK);

        group.MapGet("/GetStudents/{id}", async (int id, ICourseRepository repo, IMapper mapper) =>
        {
            var courses = await repo.GetStudentList(id);
            return mapper.Map<CourseDetailsDto>(courses);
        })
        .WithName("GetCourseDetailById")
        .WithOpenApi()
        .Produces<CourseDetailsDto>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (int id, ICourseRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Course model
                    ? Results.Ok(mapper.Map<CourseDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetCourseById")
        .WithOpenApi()
        .Produces<CourseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async  (int id, CourseDto courseDto, ICourseRepository repo, IMapper mapper) =>
        {
            var foundModel = await repo.GetAsync(id);
            if(foundModel is null)
            {
                return Results.NotFound();
            }

            mapper.Map(courseDto, foundModel);
            await repo.UpdateAsync(foundModel);
            return Results.NoContent();
        })
        .WithName("UpdateCourse")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (CreateCourseDto courseDto, ICourseRepository repo, IMapper mapper) =>
        {
            Course course = mapper.Map<Course>(courseDto);
            await repo.AddAsync(course);

            var resultCourseDto = mapper.Map<CourseDto>(course);
            return Results.Created($"/api/Course/{course.Id}", resultCourseDto);
        })
        .WithName("CreateCourse")
        .WithOpenApi()
        .Produces<CourseDto>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async  (int id, ICourseRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteCourse")
        .WithOpenApi()
        .Produces<CourseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
