using Backend.Data;
using Backend.Errors;
using Microsoft.AspNetCore.Mvc;

////50. this controller is for testing error api
namespace Backend.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class BuggyController : BaseApiController
    {
        ////connect to db
        private readonly StoreDBContext _storeDBContext;
        public BuggyController(StoreDBContext storeDBContext)
        {
            _storeDBContext = storeDBContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            //50. pretent to find a not-exist product 42
            var thing = _storeDBContext.Products.Find(42);

            if(thing == null)
            {
                ////set for test 51 Errors/ApiResponse.cs
                return NotFound(new ApiResponse(404));
            } 

            return Ok();
        }
        
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _storeDBContext.Products.Find(42);

            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            ////set for test 51 Errors/ApiResponse.cs
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}