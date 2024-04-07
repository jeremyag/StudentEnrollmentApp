﻿using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data;
using AutoMapper;
using StudentEnrollment.Api.Dtos.Enrollment;
using StudentEnrollment.Data.Contracts;
namespace StudentEnrollment.Api.Endpoints;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Enrollment").WithTags(nameof(Enrollment));

        group.MapGet("/", async (IEnrollmentRepository repo, IMapper mapper) =>
        {
            var enrollments = await repo.GetAllAsync();
            return mapper.Map<EnrollmentDto>(enrollments);
        })
        .WithName("GetAllEnrollments")
        .WithOpenApi()
        .Produces<List<EnrollmentDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (int id, IEnrollmentRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Enrollment model
                    ? Results.Ok(mapper.Map<EnrollmentDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetEnrollmentById")
        .WithOpenApi()
        .Produces<EnrollmentDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async  (int id, EnrollmentDto enrollmentDto, IEnrollmentRepository repo, IMapper mapper) =>
        {
            var foundModel = await repo.GetAsync(id);
            if(foundModel is null)
            {
                return Results.NotFound();
            }

            mapper.Map(enrollmentDto, foundModel);
            await repo.UpdateAsync(foundModel);

            return Results.NoContent();
        })
        .WithName("UpdateEnrollment")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (CreateEnrollmentDto enrollmentDto, IEnrollmentRepository repo, IMapper mapper) =>
        {
            var enrollment = mapper.Map<Enrollment>(enrollmentDto);
            await repo.AddAsync(enrollment);
            return Results.Created($"/api/Enrollment/{enrollment.Id}",enrollment);
        })
        .WithName("CreateEnrollment")
        .WithOpenApi()
        .Produces<Enrollment>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async  (int id, IEnrollmentRepository repo, IMapper mapper) =>
        {
            return await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteEnrollment")
        .WithOpenApi()
        .Produces<Enrollment>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
