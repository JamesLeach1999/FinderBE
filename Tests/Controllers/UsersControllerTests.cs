using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinderBE.ServiceHost;
using FinderBE.Controllers;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using FinderBE.Models;

namespace Tests.Controllers;
public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersControllerTests(WebApplicationFactory<Program> factory) { 
        _client = factory.CreateClient(); 
    }

    [Fact]
    public async void Given_ACallToTheStubEndpoint_When_Executed_Then_StaticDataReturned()
    {
        var response = await _client.GetAsync("/Users/TestEndpoint");

        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<User>(jsonString, new JsonSerializerOptions {  PropertyNameCaseInsensitive = true});
        Assert.NotNull(responseJson.Username);
        Console.WriteLine(responseJson);
    }
}
