using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {

        [HttpGet("unauthorized")]
        public ActionResult GetUnauthouried()
        {
            return Unauthorized();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("internalserver")]
        public ActionResult GetInternalServerError()
        {
            throw new Exception("Internal server error");
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpPost("validationerror")]
        public ActionResult GetValidationError(Product product)
        {
            return Ok();
        }
    }
}
