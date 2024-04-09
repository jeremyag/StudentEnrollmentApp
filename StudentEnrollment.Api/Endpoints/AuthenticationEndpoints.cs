using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudentEnrollment.Api.Dtos.Authentication;
using StudentEnrollment.Api.Dtos.Course;
using StudentEnrollment.Api.Services;
using StudentEnrollment.Data;
using StudentEnrollment.Data.Contracts;

namespace StudentEnrollment.Api.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/login").WithTags("Authentication");

        group.MapPost("/", async (LoginDto loginDto, IAuthManager authManager) =>
        {
            // Generate Token here..
            var response = await authManager.Login(loginDto);

            if(response is null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(response);
        })
        .WithName("Login")
        .WithOpenApi()
        .Produces(StatusCodes.Status200OK);
    }
}
