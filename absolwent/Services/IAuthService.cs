using absolwent.Models;

namespace absolwent.Services
{
    public interface IAuthService
    {
        AuthenticateResponse? Authenticate(AuthenticateRequest model);
    }
}