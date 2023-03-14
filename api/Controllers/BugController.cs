using System.Net;
using api.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;


public class BugController : BaseApiController
{
    public StoreContext _context { get; set; }
    public BugController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var thing = _context.Products.Find(43);
        if (thing == null) { return NotFound(new ApiResponse(404)); }

        return Ok();
    }
    [HttpGet("servererror")]
    public ActionResult GetServerErrorRequest()
    {
        var thing = _context.Products.Find(43);

        var thingtoreturn = thing.ToString();

        return Ok();
    }
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetNotFoundRequest(int id)
    {
        return BadRequest();
    }
}
