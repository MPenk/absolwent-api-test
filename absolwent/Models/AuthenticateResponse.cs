using absolwent.DTO;

namespace absolwent.Models
{
    public class AuthenticateResponse : Response
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse()
        {
        }
        public AuthenticateResponse(University user, string token)
        {
            Id = user.Id;
            Login = user.Login;
            Token = token;
        }
    }
}