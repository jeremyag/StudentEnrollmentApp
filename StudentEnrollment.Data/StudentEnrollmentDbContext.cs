﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudentEnrollment.Data.Configuration;

namespace StudentEnrollment.Data;

public class StudentEnrollmentDbContext : IdentityDbContext<SchoolUser>
{
    public StudentEnrollmentDbContext(DbContextOptions<StudentEnrollmentDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new CourseConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new SchoolUserConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration()); 
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
}

public class StudentEnrollmentDbContextFactory : IDesignTimeDbContextFactory<StudentEnrollmentDbContext>
{
    public StudentEnrollmentDbContext CreateDbContext(string[] args)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<StudentEnrollmentDbContext>();
        var connectionString = config.GetConnectionString("StudentEnrollmentDbConnection");
        optionsBuilder.UseSqlServer(connectionString);
        return new StudentEnrollmentDbContext(optionsBuilder.Options);
    }
}