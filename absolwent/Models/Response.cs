using Microsoft.AspNetCore.Mvc;

namespace absolwent.Models
{
    public class Response : IActionResult
    {
        public bool Error { get; set; } = false;
        public string Message { get; set; } = String.Empty;
        public int StatusCode { get; set; } = StatusCodes.Status200OK;

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this)
            {
                StatusCode = StatusCode
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }

    public class Response<T> : Response
    {
        public T? Data { get; set; }
    }
}
