using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using StudentEnrollment.Api.Dtos;
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
        var group = routes.MapGroup("/api").WithTags("Authentication");

        group.MapPost("/login/", async (IValidator<LoginDto> validator, LoginDto loginDto, IAuthManager authManager) =>
        {
            var validationResult = await validator.ValidateAsync(loginDto);

            var errors = new List<ErrorResponseDto>();
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }
            // Generate Token here..
            var response = await authManager.Login(loginDto);

            if (response is null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(response);
        })
        .AllowAnonymous()
        .WithName("Login")
        .WithOpenApi()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/register/", async (RegisterDto registerDto, IAuthManager authManager) =>
        {
            // Generate Token here..
            var response = await authManager.Register(registerDto);

            if (!response.Any())
            {
                return Results.Ok();
            }

            var errors = new List<ErrorResponseDto>();
            foreach(var error in response)
            {
                errors.Add(new ErrorResponseDto
                {
                    Code = error.Code,
                    Description = error.Description,
                });
            }

            return Results.BadRequest(errors);
        })
        .AllowAnonymous()
        .WithName("Register")
        .WithOpenApi()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
