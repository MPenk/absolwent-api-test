using absolwent.Constants;
using absolwent.DAL;
using absolwent.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace absolwent.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly Users _users;
        private readonly IUniversityRepository _universityRepository;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, Users users, IUniversityRepository universityRepository)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _users = users;
            _universityRepository = universityRepository;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null || !String.IsNullOrEmpty(token))
                attachUserToContext(context, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = long.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var user = _universityRepository.GetUniversity(userId);
                if (user != null)
                {
                    context.Items["User"] = user;
                }
                // attach user to context on successful jwt validation
                //context.Items["User"] = _users.List.Where(user => user.Value.Id == userId).First().Value;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
