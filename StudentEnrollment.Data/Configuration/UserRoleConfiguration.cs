using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.Data.Configuration;

internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "67a2d6e2-c096-4a73-8541-18e9a915c60d",
                UserId = "408aa945-3d84-4421-8342-7269ec64d949"
            },
            new IdentityUserRole<string>
            {
                RoleId = "504830c0-9bce-4981-a8a8-e2d47a4f7db2",
                UserId = "3f4631bd-f907-4409-b416-ba356312e659"
            });
    }
}
