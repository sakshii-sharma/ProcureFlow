using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcureFlow.API.Common.Responses;

namespace ProcureFlow.API.Common.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult<ApiResponse<T>> OkResponse<T>(T data, string message)
        {
            return Ok(ApiResponse<T>.SuccessResponse(data, message));
        }

        protected ActionResult<ApiResponse<T>> CreatedResponse<T>(T data, string message)
        {
            return StatusCode(StatusCodes.Status201Created, ApiResponse<T>.SuccessResponse(data, message));
        }
    }
}

