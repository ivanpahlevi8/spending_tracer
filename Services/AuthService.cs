using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpendTracerApi.Data;
using SpendTracerApi.Models;
using SpendTracerApi.Models.Dtos;
using SpendTracerApi.Services.IServices;

namespace SpendTracerApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IJwtService _jwtService;
        private ResponseDto _responseDto;

        public AuthService(UserManager<UserModel> userManager, AppDbContext dbContext, IJwtService jwtService)
        {
            _userManager = userManager;
            _dbContext = dbContext;

            _responseDto = new ResponseDto
            {
                message = "",
                isSuccess = false,
                result = null,
            };
            _jwtService = jwtService;
        }

        public async Task<ResponseDto> LoginUser(LoginUserRequestDto loginDto)
        {
            try
            {
                // get user based on username
                UserModel? getUser = await _dbContext.User.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);

                if(getUser == null)
                {
                    _responseDto.message = $"User with username {loginDto.UserName} is not exist";

                    return _responseDto;
                }

                // compare password
                bool isPasswordCorrect = await _userManager.CheckPasswordAsync(getUser, loginDto.Password);

                if(isPasswordCorrect)
                {
                    // create token
                    string getToken = _jwtService.GenerateToken(getUser);

                    // create login response
                    LoginUserResponseDto loginResponse = new LoginUserResponseDto
                    {
                        User = getUser,
                        Token = getToken
                    };

                    _responseDto.message = "Success Login";
                    _responseDto.isSuccess = true;
                    _responseDto.result = loginResponse;

                    return _responseDto;
                } else
                {
                    _responseDto.message = "Password not match";

                    return _responseDto;
                }
            } catch (Exception ex)
            {
                _responseDto.message = ex.InnerException != null
                ? $"{ex.Message} - {ex.InnerException.Message}"
                : ex.Message;

                return _responseDto;
            }
        }

        public async Task<ResponseDto> RegisterUser(RegisterUserDto registerUserDto)
        {
            try
            {
                // validate first, is username or email already registered
                UserModel? getUserValidated = _dbContext.User.FirstOrDefault(u => u.UserName == registerUserDto.UserName || u.Email == registerUserDto.Email);

                if(getUserValidated != null)
                {
                    // user already registered
                    _responseDto.message = "Username or email already registered";
                    _responseDto.isSuccess = false;

                    return _responseDto;
                }

                // create user model
                UserModel userModel = new UserModel
                {
                    FirstName = registerUserDto.FirstName,
                    LastName = registerUserDto.LastName,
                    Email = registerUserDto.Email,
                    PhoneNumber = registerUserDto.PhoneNumber,
                    NormalizedEmail = registerUserDto.Email,
                    UserName = registerUserDto.UserName,
                };

                // create user
                var response = await _userManager.CreateAsync(userModel, registerUserDto.Password);

                // check for response
                if(response.Succeeded)
                {
                    // get user
                    UserModel? getUser = await _dbContext.User.FirstOrDefaultAsync(u => u.UserName == registerUserDto.UserName);

                    if(getUser != null)
                    {
                        _responseDto.message = "Error when register, regsiter user not reached db";

                        return _responseDto;
                    }

                    _responseDto.message = "Success register as new user";
                    _responseDto.isSuccess = true;
                    _responseDto.result = getUser;

                    return _responseDto;
                } else
                {
                    // get response error
                    string responseError = response.Errors.FirstOrDefault()?.Description ?? "Unknown registration error";

                    _responseDto.message = responseError;

                    return _responseDto;
                }
            } catch (Exception ex)
            {
                _responseDto.message = ex.InnerException != null
                ? $"{ex.Message} - {ex.InnerException.Message}"
                : ex.Message;

                return _responseDto;
            }
        }
    }
}
