using Challenges.Api.Mapping;
using Challenges.Application.Repositories;
using Challenges.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Challenges.Api.Controllers;

[ApiController]
public class ChallengesController : ControllerBase
{
    private readonly IChallengeRepository _challengeRepository;

    public ChallengesController(IChallengeRepository challengeRepository)
    {
        _challengeRepository = challengeRepository;
    }

    [HttpPost(ApiEndpoints.Challenges.Create)]
    public async Task<IActionResult> Create([FromBody] CreateChallengeRequest request)
    {
        var challenge = request.MapToChallenge();

        await _challengeRepository.CreateAsync(challenge);

        return CreatedAtAction(nameof(Get), new { id = challenge.Id }, challenge);
    }

    [HttpGet(ApiEndpoints.Challenges.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var challenge = await _challengeRepository.GetByIdAsync(id);

        if (challenge is null)
        {
            return NotFound();
        }

        var response = challenge.MapToResponse();

        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Challenges.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var challenges = await _challengeRepository.GetAllAsync();

        var response = challenges.MapToResponse();

        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Challenges.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateChallengeRequest request)
    {
        var challenge = request.MapToChallenge(id);
        var updated = await _challengeRepository.UpdateAsync(challenge);

        if (!updated)
        {
            return NotFound();
        }

        var response = challenge.MapToResponse();

        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Challenges.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _challengeRepository.DeleteByIdAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}