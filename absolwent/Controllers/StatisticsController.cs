using absolwent.DAL;
using absolwent.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace absolwent.Controllers
{
    [Route("api/public/[controller]")]
    [ApiExplorerSettings(GroupName = "public")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        public StatisticsController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        /// <summary>
        /// Pobieranie Danych odnośnie zarobków.
        /// </summary>
        /// <param name="year">Rok</param>
        /// <param name="gender">Płeć</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Success", typeof(Response<Statistics>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(403, "Forbidden", typeof(Response))]
        [HttpGet("salary/{year}")]
        public Response<Statistics> GetSalaryByYear(int year, string? gender = null)
        {
            var response = _dataRepository.GetStatistics("earnings", "Przedział zarobkowy brutto", year, gender);

            long count = 0;
            foreach (var item in response.Answers)
            {
                count += item.Count;
            }

            response.AnswersCount = count;

            return new Response<Statistics> { Data = response };
        }
        /// <summary>
        /// Pobieranie Danych odnośnie Wielkości firmy.
        /// </summary>
        /// <param name="year">Rok</param>
        /// <param name="gender">Płeć</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Success", typeof(Response<Statistics>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(403, "Forbidden", typeof(Response))]
        [HttpGet("companySize/{year}")]
        public Response<Statistics> GetCompanySizeYear(int year, string? gender = null)
        {
            var response = _dataRepository.GetStatistics("companySize", "Wielkość firmy", year, gender);

            long count = 0;
            foreach (var item in response.Answers)
            {
                count += item.Count;
            }

            response.AnswersCount = count;

            return new Response<Statistics> { Data = response };
        }
        /// <summary>
        /// Pobieranie Danych odnośnie wielkości miasta.
        /// </summary>
        /// <param name="year">Rok</param>
        /// <param name="gender">Płeć</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Success", typeof(Response<Statistics>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(403, "Forbidden", typeof(Response))]
        [HttpGet("townSize/{year}")]
        public Response<Statistics> GetTownSizeyByYear(int year, string? gender = null)
        {
            var response = _dataRepository.GetStatistics("townSize", "Wielkość miasta", year, gender);

            long count = 0;
            foreach (var item in response.Answers)
            {
                count += item.Count;
            }

            response.AnswersCount = count;

            return new Response<Statistics> { Data = response };
        }
        /// <summary>
        /// Pobieranie Danych odnośnie kategorii firmy.
        /// </summary>
        /// <param name="year">Rok</param>
        /// <param name="gender">Płeć</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Success", typeof(Response<Statistics>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(403, "Forbidden", typeof(Response))]
        [HttpGet("companyCategory/{year}")]
        public Response<Statistics> GetCompanyCategoryByYear(int year, string? gender = null)
        {
            var response = _dataRepository.GetStatistics("companyCategory", "Kategoria firmy", year, gender);

            long count = 0;
            foreach (var item in response.Answers)
            {
                count += item.Count;
            }

            response.AnswersCount = count;

            return new Response<Statistics> { Data = response };
        }

        /// <summary>
        /// Pobieranie Danych odnośnie długości szukania pracy.
        /// </summary>
        /// <param name="year">Rok</param>
        /// <param name="gender">Płeć</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Success", typeof(Response<Statistics>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(403, "Forbidden", typeof(Response))]
        [HttpGet("jobSearchTime/{year}")]
        public Response<Statistics> GetJobSearchTimeByYear(int year, string? gender = null)
        {
            var response = _dataRepository.GetStatistics("jobSearchTime", "Czas szukania pracy", year, gender);

            long count = 0;
            foreach (var item in response.Answers)
            {
                count += item.Count;
            }

            response.AnswersCount = count;

            return new Response<Statistics> { Data = response };
        }
        /// <summary>
        /// Pobieranie Danych odnośnie długości aktualnego zatrudnienia.
        /// </summary>
        /// <param name="year">Rok</param>
        /// <param name="gender">Płeć</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Success", typeof(Response<Statistics>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(403, "Forbidden", typeof(Response))]
        [HttpGet("periodOfEmployment/{year}")]
        public Response<Statistics> GetPeriodOfEmploymentByYear(int year, string? gender = null)
        {
            var response = _dataRepository.GetStatistics("periodOfEmployment", "Długość aktualnego zatrudnienia", year, gender);

            long count = 0;
            foreach (var item in response.Answers)
            {
                count += item.Count;
            }

            response.AnswersCount = count;

            return new Response<Statistics> { Data = response };
        }
        /// <summary>
        /// Pobieranie Danych odnośnie zadowolenia z pracy.
        /// </summary>
        /// <param name="year">Rok</param>
        /// <param name="gender">Płeć</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Success", typeof(Response<Statistics>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(403, "Forbidden", typeof(Response))]
        [HttpGet("jobSatisfaction/{year}")]
        public Response<Statistics> GetJobSatisfactionByYear(int year, string? gender = null)
        {
            var response = _dataRepository.GetStatistics("jobSatisfaction", "Satysfkacja z pracy", year, gender);

            long count = 0;
            foreach (var item in response.Answers)
            {
                count += item.Count;
            }

            response.AnswersCount = count;

            return new Response<Statistics> { Data = response };
        }

        /// <summary>
        /// Pobieranie Danych odnośnie doszkalania się po studiach.
        /// </summary>
        /// <param name="year">Rok</param>
        /// <param name="gender">Płeć</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Success", typeof(Response<Statistics>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(403, "Forbidden", typeof(Response))]
        [HttpGet("training/{year}")]
        public Response<Statistics> GetTrainingByYear(int year, string? gender = null)
        {
            var response = _dataRepository.GetStatistics("Training", "Doszkalanie się w zawodzie po ukończeniu studiów", year, gender);

            long count = 0;
            foreach (var item in response.Answers)
            {
                count += item.Count;
            }

            response.AnswersCount = count;

            return new Response<Statistics> { Data = response };
        }

        // <summary>
        /// Pobieranie Danych odnośnie doszkalania się po studiach.
        /// </summary>
        /// <param name="year">Rok</param>
        /// <param name="gender">Płeć</param>
        /// <returns></returns>
        [SwaggerResponse(200, "Success", typeof(Response<Statistics>))]
        [SwaggerResponse(401, "Unauthorized", typeof(Response))]
        [SwaggerResponse(403, "Forbidden", typeof(Response))]
        [HttpGet("location/{year}")]
        public Response<Statistics> GetLocationByYear(int year, string? gender = null)
        {
            var response = _dataRepository.GetStatistics("location", "Czy praca na terenie Polski", year, gender);

            long count = 0;
            foreach (var item in response.Answers)
            {
                count += item.Count;
            }

            response.AnswersCount = count;

            return new Response<Statistics> { Data = response };
        }
    }



    public enum Gender
    {
        MAN, WOMAN
    }
}
