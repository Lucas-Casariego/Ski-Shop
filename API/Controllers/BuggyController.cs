using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class BuggyController : BaseApiController
{
    [HttpGet("not-found")]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        return BadRequest(new ProblemDetails { Title = "This is a bad request"});
    }

    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("validation-error")]
    public IActionResult GetValidationError()
    {
        ModelState.AddModelError("Problem1", "this is the first error");
        ModelState.AddModelError("Problem2", "this is the second error");
        return ValidationProblem();
    }

    [HttpGet("server-error")]
    public IActionResult GetServerError()
    {
        throw new Exception("this is a server error");
    }
}
