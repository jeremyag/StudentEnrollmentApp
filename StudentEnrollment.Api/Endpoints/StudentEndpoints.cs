﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollment.Data;
using AutoMapper;
using StudentEnrollment.Api.Dtos.Student;
using StudentEnrollment.Data.Contracts;
namespace StudentEnrollment.Api.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (IStudentRepository repo, IMapper mapper) =>
        {
            var students = await repo.GetAllAsync();
            return mapper.Map<List<StudentDto>>(students);
        })
        .WithName("GetAllStudents")
        .WithOpenApi()
        .Produces<List<StudentDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, IStudentRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Student model
                    ? Results.Ok(mapper.Map<StudentDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetStudentById")
        .WithOpenApi()
        .Produces<StudentDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/Student/GetDetails/{id}", async (int id, IStudentRepository repo, IMapper mapper) =>
        {
            return await repo.GetStudentDetails(id)
                is Student model
                    ? Results.Ok(mapper.Map<StudentDetailsDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetStudentDetailsById")
        .WithOpenApi()
        .Produces<StudentDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, StudentDto studentDto, IStudentRepository repo, IMapper mapper) =>
        {
            var foundModel = await repo.GetAsync(id);
            if(foundModel is null)
            {
                return Results.NotFound();
            }

            mapper.Map(studentDto, foundModel);
            repo.UpdateAsync(foundModel);
            return Results.NoContent();
        })
        .WithName("UpdateStudent")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (CreateStudentDto studentDto, IStudentRepository repo, IMapper mapper) =>
        {
            var student = mapper.Map<Student>(studentDto);
            await repo.AddAsync(student);
            return Results.Created($"/api/Student/{student.Id}", student);
        })
        .WithName("CreateStudent")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, IStudentRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteStudent")
        .WithOpenApi()
        .Produces<Student>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
