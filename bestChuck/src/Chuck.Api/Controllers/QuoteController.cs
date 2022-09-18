using Chuck.Api.Models;
using Chuck.Application.Features.GetQuotes;
using Chuck.Application.Features.GetTop10;
using Chuck.Application.Features.Reset;
using Chuck.Application.Features.Vote;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chuck.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class QuoteController : ControllerBase
{

    private readonly ILogger<QuoteController> _logger;
    private readonly IMediator _mediator;

    public QuoteController(IMediator mediator, ILogger<QuoteController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [Authorize(Roles = "Admin,User")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetQuotesQuery());
        return Ok(result); //result should be mapped to response dto 
    }

    [Authorize(Roles = "Admin,User")]
    [HttpPost("vote")]
    public async Task<IActionResult> Vote([FromBody] VoteRequest vote)
    {
        await _mediator.Send(new VoteCommand(vote.QuoteId));
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("reset")]
    public async Task<IActionResult> Reset()
    {
        await _mediator.Send(new ResetCommand());
        return Ok();
    }

    [Authorize(Roles = "Admin,User")]
    [HttpGet("top10")]
    public async Task<IActionResult> GetTop10()
    {
        var result = await _mediator.Send(new GetTop10Query());
        return Ok(result); //result should be mapped to response dto
    }
}
