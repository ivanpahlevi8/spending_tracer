namespace SpendTracerApi.Models.Dtos
{
    public class LoginUserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginUserRequestDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
