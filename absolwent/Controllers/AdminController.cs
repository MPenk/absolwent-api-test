using absolwent.Attributes;
using absolwent.DAL;
using absolwent.DTO;
using absolwent.Models;
using absolwent.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mail;
using Graduate = absolwent.DTO.Graduate;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace absolwent.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IGraduateRepository _graduateRepository;
        private readonly IQuestionnaireRepository _questionnaireRepository;
        private readonly IDataRepository _dataRepository;
        public AdminController(IUniversityRepository universityRepository, IGraduateRepository graduateRepository, IQuestionnaireRepository questionnaireRepository, IDataRepository dataRepository)
        {
            _universityRepository = universityRepository;
            _graduateRepository = graduateRepository;
            _questionnaireRepository = questionnaireRepository;
            _dataRepository = dataRepository;
        }

        // POST api/<AdminController>
        /// <summary>
        /// Wysyłanie ankiet
        /// </summary>
        /// <param name="frequency">Częstotliwość (podana w miesiącach)</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("survey")]
        [SwaggerResponse(200, "Success", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        public IActionResult SendSurvey([FromBody] PoolSettings pool)
        {
            University user = (University)this.HttpContext.Items["User"];
            _universityRepository.ChangeFrequency(user.Id, pool.Frequency);
            //try
            //{
            //    _poolService.StartPool(user, pool);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new Response() { Error = true, Message = ex.Message, StatusCode = 500 });
            //}
            return Ok(new Response());
        }



        /// <summary>
        /// Dodawanie absolwenta
        /// </summary>
        /// <param name="graduate">Dane absolwenta</param>
        /// <returns></returns>
        [HttpPost("graduate")]
        [Authorize]
        [SwaggerResponse(201, "Created", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(409, "User exist", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> AddGraduate([FromBody] Graduate graduate)
        {
            try
            {
                _graduateRepository.CreateGraduate(graduate);
            }
            catch (Exception ex)
            {
                return new Response { Error = true, Message = ex.Message, StatusCode = 409 };
            }
            return new Response() { StatusCode = 201 };
        }

        /// <summary>
        /// Wysłanie e-maila do zmiany hasła
        /// </summary>
        /// <param name="resetEmail">Email do resetu hasła</param>
        /// <returns></returns>
        [HttpPatch("password/reset")]
        [SwaggerResponse(200, "Success", typeof(Response))]
        [SwaggerResponse(404, "UserDoesNotExist", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public IActionResult SendEmailToResetPassword([FromBody] PasswordResetEmail resetEmail)
        {
            var user = _universityRepository.GetUniversity(resetEmail.Email);
            if (user == null)
                return new Response { Error = true, Message = "Konto nie istnieje", StatusCode = 404 };
            var key = _universityRepository.CreatePasswordResetKey(resetEmail.Email);
            if (key == null)
                return new Response { Error = true, Message = "Błąd generowania linku", StatusCode = 400 };

            var message = new MailMessage("absolwent.best@gmail.com", resetEmail.Email, "Reset hasła", $"Witaj, oto link do resetu hasła: https://dev.absolwent.best/admin/password/reset?token={key}");
            PoolService.SendMail(message);
            return new Response();
        }

        /// <summary>
        /// Zmiana hasła
        /// </summary>
        /// <param name="data">Dane do zmiany hasła</param>
        /// <returns></returns>
        [HttpPatch("password")]
        [SwaggerResponse(200, "Success", typeof(Response))]
        [SwaggerResponse(400, "Weak password", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public IActionResult ChangePassword([FromBody] PasswordSet data)
        {
            if (data.Password.Count() < 8)
                return new Response { Error = true, Message = "Słabe hasło", StatusCode = 400 };
            try
            {
                _universityRepository.PasswordReset(data.Password, data.Token);

            }
            catch (Exception ex)
            {

                return new Response { Error = true, Message = ex.Message, StatusCode = 401 };

            }

            return new Response();
        }

        /// <summary>
        /// Create Admin
        /// </summary>
        /// <param name="data">Dane do utworzenia admina</param>
        /// <returns></returns>
        [HttpPatch("create")]
        [SwaggerResponse(200, "Success", typeof(Response))]
        [SwaggerResponse(400, "Weak password", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public IActionResult CreateAdmin([FromBody] University data)
        {
            if (data.Password.Count() < 8)
                return new Response { Error = true, Message = "Słabe hasło", StatusCode = 400 };
            data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            _universityRepository.CreateUniversity(data);
            return new Response() { Message = "Created", StatusCode = 201 };
        }

        /// <summary>
        /// Generate data
        /// </summary>
        /// <param name="data">Dane do utworzenia admina</param>
        /// <returns></returns>
        [HttpPatch("create/random")]
        [SwaggerResponse(200, "Success", typeof(Response))]
        [SwaggerResponse(400, "Weak password", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public IActionResult GenerateData([FromBody] int count)
        {
            _dataRepository.CreateRandom(count);
            return new Response() { Message = "Created", StatusCode = 201 };
        }

        // POST api/<AdminController>
        /// <summary>
        /// Wysyłanie ankiet teraz
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("survey/send")]
        [SwaggerResponse(200, "Success", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        public IActionResult SendSurveyNow([FromBody] SurveySendNowRequest value)
        {
            new Task(() => { _questionnaireRepository.CreateForYear(value.Year); }).Start();
            //try
            //{
            //    _poolService.StartPool(user, pool);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new Response() { Error = true, Message = ex.Message, StatusCode = 500 });
            //}
            return Ok(new Response());
        }


        // POST api/<AdminController>
        /// <summary>
        /// Lista absolwentów
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("graduate/list")]
        [SwaggerResponse(200, "Success", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        public IActionResult GraduateList()
        {
            var list = _graduateRepository.GetGraduates();
            return Ok(new GraduatesListResponse() { Graduates = list });
        }

        // POST api/<AdminController>
        /// <summary>
        /// Usunięcie absolwenta
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("graduate/delete")]
        [SwaggerResponse(200, "Success", typeof(Response))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        public IActionResult GraduateRemove([FromBody] GraduateRemove data)
        {
            try
            {
                _graduateRepository.DeleteGraduate(data.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new Response() { Error = true, Message = ex.Message, StatusCode = 500 });
            }
            return Ok(new Response());
        }
    }
}
