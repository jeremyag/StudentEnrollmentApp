using StudentEnrollment.Api.Dtos.Authentication;

namespace StudentEnrollment.Api.Services;

public interface IAuthManager
{
    Task<AuthResponseDto> Login(LoginDto loginDto);
}
