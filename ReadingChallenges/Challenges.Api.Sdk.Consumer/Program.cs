
using Challenges.Api.Sdk;
using Challenges.Contracts.Requests;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Text.Json;

//var challengesApi = RestService.For<IChallengesApi>("https://localhost:7192");

var services = new ServiceCollection();

services.AddRefitClient<IChallengesApi>()
    .ConfigureHttpClient(x =>
        x.BaseAddress = new Uri("https://localhost:7192"));

var provider = services.BuildServiceProvider();

var challengesApi = provider.GetRequiredService<IChallengesApi>();

//var challenges = await challengesApi.GetAllAsync();

var newChallenge = await challengesApi.CreateAsync(new CreateChallengeRequest
{
    Name = "testing 2",
    Target = 100,
    Completed = 0,
    Year = 2025
});

Console.WriteLine(JsonSerializer.Serialize(newChallenge));

var updatedChallenge = await challengesApi.UpdateAsync(newChallenge.Id.ToString(), new UpdateChallengeRequest
{
    Name = "testing 2",
    Target = 150,
    Completed = 0,
    Year = 2025
});


Console.WriteLine(JsonSerializer.Serialize(updatedChallenge));

Console.ReadLine();
