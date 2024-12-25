using FinderBE.Domain;
using FinderBE.Helpers;
using FinderBE.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace FinderBE.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController(IGetValues<User> userDb, AbstractValidator<Guid> userIdValidator) : Controller
{

    /// <summary>
    /// Endpoint to get all users in the table
    /// </summary>
    /// <returns>List of all users</returns>
    /// <response code="200">When the call is successful</response>
    /// <response code="500">For any other error</response>
    [HttpGet]
    [Route("GetUsers")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var allUsers = await userDb.GetValues();

        return Ok(allUsers);
    }

    /// <summary>
    /// Endpoint to get a single user based on their ID
    /// </summary>
    /// <returns>List of all users</returns>
    /// <response code="200">When the call is successful</response>
    /// <response code="400">When the Guid format is invalid</response>
    /// <response code="404">When no user with that ID is found</response>
    /// <response code="500">For any other error</response>
    [HttpGet]
    [Route("GetUser")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<User>> GetUser([FromQuery] Guid userId)
    {
        try
        {
            var validUserId = await userIdValidator.ValidateAsync(userId);

            if (!validUserId.IsValid)
            {
                return BadRequest("Invalid user id");
            }

            var user = await userDb.GetValue(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500);
        }

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
        var user = new { UserId = Guid.NewGuid(), Email = emailString, AccountCreatedDate = DateTime.Now, Password = passwordString, PhoneNumber = phone, Username = CredentialsGenerator.GenerateRandomString(6) };
        return Ok(user);
    }
}