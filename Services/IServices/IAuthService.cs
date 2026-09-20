using SpendTracerApi.Models;
using SpendTracerApi.Models.Dtos;

namespace SpendTracerApi.Services.IServices
{
    public interface IAuthService
    {
        // function to register as a new user
        public Task<ResponseDto> RegisterUser(RegisterUserDto registerUserDto);

        // function to login
        public Task<ResponseDto> LoginUser(LoginUserRequestDto loginDto);
    }
}
