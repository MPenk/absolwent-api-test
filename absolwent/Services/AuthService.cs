using absolwent.Constants;
using absolwent.DAL;
using absolwent.DTO;
using absolwent.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace absolwent.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly Users _users;
        private readonly IUniversityRepository _universityRepository;
        public AuthService(IOptions<AppSettings> appSettings, Users users, IUniversityRepository universityRepository)
        {
            _appSettings = appSettings.Value;
            _users = users;
            _universityRepository = universityRepository;
        }
        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {
            var user = _universityRepository.GetUniversity(model.Login);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return null;

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        private string generateJwtToken(University user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Claims = new Dictionary<string, object> { { "email", user.Login } }
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
