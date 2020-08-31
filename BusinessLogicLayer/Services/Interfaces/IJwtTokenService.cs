namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId);
    }
}