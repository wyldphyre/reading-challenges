using Challenges.Api.Mapping;
using Challenges.Application.Services;
using Challenges.Contracts.Requests;
using Challenges.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Challenges.Api.Controllers;

[ApiController]
public class ChallengesController : ControllerBase
{
    private readonly IChallengeService _challengeService;

    public ChallengesController(IChallengeService challengeService)
    {
        _challengeService = challengeService;
    }

    [HttpPost(ApiEndpoints.Challenges.Create)]
    [ProducesResponseType(typeof(ChallengeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateChallengeRequest request,
        CancellationToken token)
    {
        var challenge = request.MapToChallenge();

        await _challengeService.CreateAsync(challenge, token);

        var response = challenge.MapToResponse();
        return CreatedAtAction(nameof(Get), new { id = challenge.Id }, response);
    }

    [HttpGet(ApiEndpoints.Challenges.Get)]
    //[ResponseCache(Duration = 30, VaryByHeader = "Accept, Accept-Encoding", Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(ChallengeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id,
        CancellationToken token)
    {
        var challenge = await _challengeService.GetByIdAsync(id, token);

        if (challenge is null)
        {
            return NotFound();
        }

        var response = challenge.MapToResponse();

        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Challenges.GetAll)]
    //[ResponseCache(Duration = 30, VaryByQueryKeys = new[] { "name", "year", "target", "completed" }, VaryByHeader = "Accept, Accept-Encoding", Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(ChallengesResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var challenges = await _challengeService.GetAllAsync(token);

        var response = challenges.MapToResponse();

        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Challenges.Update)]
    [ProducesResponseType(typeof(ChallengeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateChallengeRequest request,
        CancellationToken token)
    {
        var challenge = request.MapToChallenge(id);
        var updatedChallenge = await _challengeService.UpdateAsync(challenge, token);

        if (updatedChallenge is null)
        {
            return NotFound();
        }

        var response = challenge.MapToResponse();

        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Challenges.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken token)
    {
        var deleted = await _challengeService.DeleteByIdAsync(id, token);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}