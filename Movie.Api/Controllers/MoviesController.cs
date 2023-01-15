using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.Api.Query;
using Movie.Api.Query.MovieDetails;

namespace Movie.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? query, [FromQuery] int? page,
        [FromQuery] int? itemCount)
    {
        var result = await _mediator.Send(new GetMovieListQuery(query)
        {
            Page = page ?? 1,
            ItemCount = itemCount ?? 10
        });
        return Ok(result);
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAll([FromRoute, Required] string id)
    {
        var result = await _mediator.Send(new GetMovieDetailsQuery
        {
            Id = id
        });

        if (result is null)
            return NotFound();
        
        return Ok(result);
    }
}