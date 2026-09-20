using SpendTracerApi.Models;

namespace SpendTracerApi.Services.IServices
{
    public interface IJwtService
    {
        public string GenerateToken(UserModel userModel);
    }
}
