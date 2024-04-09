using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.Data.Configuration;

internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "67a2d6e2-c096-4a73-8541-18e9a915c60d",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "504830c0-9bce-4981-a8a8-e2d47a4f7db2",
                Name = "User",
                NormalizedName = "USER"
            }
        );
    }
}
