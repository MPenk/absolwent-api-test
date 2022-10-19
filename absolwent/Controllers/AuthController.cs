using absolwent.DAL;
using absolwent.Models;
using absolwent.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace absolwent.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IQuestionnaireRepository _questionnaireRepository;

        public AuthController(IAuthService authService, IQuestionnaireRepository questionnaireRepository)
        {
            _authService = authService;
            _questionnaireRepository = questionnaireRepository;
        }

        // POST api/<AuthController>

        /// <summary>
        /// Autoryzacja admina
        /// </summary>
        /// <param name="request">Dane logowania</param>
        /// <returns></returns>
        [HttpPost("admin")]
        [SwaggerResponse(200, "AuthenticateResponse", typeof(AuthenticateResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        public IActionResult AdminAuth([FromBody] AuthenticateRequest request)
        {
            var auth = _authService.Authenticate(request);
            if (auth == null)
                return new Response { Error = true, Message = "Niepoprawne dane", StatusCode = 401 };
            return Ok(auth);
        }

        // POST api/<AuthController>
        /// <summary>
        /// Autoryzacja ankiety
        /// </summary>
        /// <param name="data">Token</param>
        /// <returns></returns>
        [HttpPost("survey")]
        [SwaggerResponse(200, "AuthenticateResponse", typeof(SurveyAuthResponse))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        public IActionResult SurveyAuth([FromBody] SurveyAuth data)
        {
            try
            {
                var user = _questionnaireRepository.CheckToken(data.Token);

                return Ok(new SurveyAuthResponse
                {
                    Data = user
                });
            }
            catch (Exception ex)
            {

                return new Response { Error = true, Message = ex.Message, StatusCode = 401 };
            }

            return new Response { Error = true, Message = "Brak autoryzacji", StatusCode = 401 }; ;

        }


    }
}
