using FinderBE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using FinderBE.Domain;
using FinderBE.Helpers;

namespace FinderBE.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController(IGetValues<User> userDb) : Controller
{

    [HttpGet]
    [Route("GetUsers")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var test = await userDb.GetValues();

        return Ok(test);
    }
    /// <summary>
    /// This is a simple endpoint used for testing
    /// </summary>
    /// <returns>Static user object</returns>
    /// <response code="200">When the call is successful</response>
    /// <response code="500">For any other error</response>
    [HttpGet]
    [Route("TestEndpoint")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<User>> Get()
    {
        var emailString = $"{CredentialsGenerator.GenerateRandomString(5)}@{CredentialsGenerator.GenerateRandomString(5)}.com";
        var passwordString = $"{CredentialsGenerator.GeneratePassword(8)}";
        var phone = "07777777777";
        var user = new { UserId = Guid.NewGuid(), Email = emailString, AccountCreatedDate = DateTime.Now, Password = passwordString, PhoneNumber = phone, Username = CredentialsGenerator.GenerateRandomString(6)};
        return Ok(user);
    }
}
