using Challenges.Api.Mapping;
using Challenges.Application.Services;
using Challenges.Contracts.Requests;
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
    public async Task<IActionResult> Create(
        [FromBody] CreateChallengeRequest request,
        CancellationToken token)
    {
        var challenge = request.MapToChallenge();

        await _challengeService.CreateAsync(challenge, token);

        return CreatedAtAction(nameof(Get), new { id = challenge.Id }, challenge);
    }

    [HttpGet(ApiEndpoints.Challenges.Get)]
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
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var challenges = await _challengeService.GetAllAsync(token);

        var response = challenges.MapToResponse();

        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Challenges.Update)]
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