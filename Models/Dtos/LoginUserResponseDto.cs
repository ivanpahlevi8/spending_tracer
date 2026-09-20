namespace SpendTracerApi.Models.Dtos
{
    public class LoginUserResponseDto
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
    }
}
