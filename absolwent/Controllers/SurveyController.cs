// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using absolwent.DAL;
using absolwent.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace absolwent.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "survey")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public SurveyController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        // POST api/<SurveyController>
        /// <summary>
        /// Wysłanie wypełnionej ankiety
        /// </summary>
        /// <param name="data">Dane z ankiety</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(200, "Success", typeof(OkResult))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(400, "Bad request", typeof(Response))]
        public IActionResult Send([FromBody] SurveyCreate data)
        {
            try
            {
                _dataRepository.Create(data);
            }
            catch (Exception ex)
            {

                return new Response { Error = true, Message = ex.Message, StatusCode = 400 };
            }
            return Ok(new Response());
        }
    }
}
