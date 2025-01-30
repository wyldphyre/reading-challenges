using Challenges.Contracts.Requests;
using Challenges.Contracts.Responses;
using Refit;

namespace Challenges.Api.Sdk;

/// <summary>
/// This interface is used by the Refit package to generate the SDK implementation
/// </summary>
public interface IChallengesApi
{
    [Post(ApiEndpoints.Challenges.Create)]
    Task<ChallengeResponse> CreateAsync(CreateChallengeRequest request);

    [Get(ApiEndpoints.Challenges.Get)]
    Task<ChallengeResponse> GetChallengeAsync(string id);

    [Get(ApiEndpoints.Challenges.GetAll)]
    Task<ChallengesResponse> GetAllAsync();

    [Put(ApiEndpoints.Challenges.Update)]
    Task<ChallengeResponse> UpdateAsync(string id, UpdateChallengeRequest request);

    [Delete(ApiEndpoints.Challenges.Delete)]
    Task<bool> DeleteAsync(string id);
}