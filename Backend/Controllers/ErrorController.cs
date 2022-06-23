
using Backend.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    //////52. Adding a not found endpoint error handler
    [Route("errors/{code}")]
    //////55. Adding Swagger for documenting our API
    [ApiExplorerSettings(IgnoreApi = true)]  //if no this setting, swagger will show error and cannot load json data
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)  ////code from Route
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}