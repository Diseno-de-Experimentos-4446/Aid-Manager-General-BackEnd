using System.Net.Mime;
using AidManager.API.Authentication.Domain.Model.Commands;
using AidManager.API.Authentication.Domain.Model.Queries;
using AidManager.API.Authentication.Domain.Services;
using AidManager.API.Authentication.Interfaces.REST.Resources;
using AidManager.API.Authentication.Interfaces.REST.Transform;
using AidManager.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using AidManager.API.UserProfile.Domain.Model.Commands;
using AidManager.API.UserProfile.Interfaces.REST.Resources;
using AidManager.API.UserProfile.Interfaces.REST.Transform;
using Google.Protobuf;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AidManager.API.Authentication.Interfaces;

[Authorize]
[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UsersController(IUserCommandService userCommandService, IUserQueryService userQueryService) : ControllerBase
{

    [HttpGet]
    [SwaggerOperation(
        Summary = "Obtains all users",
        Description = "Obtains all users",
        OperationId = "GetAllUsers"
    )]
    [SwaggerResponse(200, "The users were obtained")]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var users = await userQueryService.Handle(query);
        if(users == null) return BadRequest();
        var usersResources = users.Select(CreateResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(usersResources);
    }
    

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromQuery] string email, [FromBody] UpdateUserResource resource)
    {
        var query = new GetUserByEmailQuery(email);
        var user = await userQueryService.FindUserByEmail(query);
        if(user == null) return BadRequest();
        
        var command = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var updatedUser = await userCommandService.Handle(command,  email);
        if(updatedUser == null) return BadRequest();
        
        var userResource = CreateResourceFromEntityAssembler.ToResourceFromEntity(updatedUser);
        return Ok(userResource);
    }
    
    
    [HttpPut("kick-member/{userId}")]
    public async Task<IActionResult> KickUserByCompanyId(int userId)
    {
        if (!await userCommandService.Handle(new KickUserByCompanyIdCommand(userId)))
            return Ok(new { status_code = 404, message = "User not found"});
        return Ok(new {status_code=202, message = "User kicked"});
    }


    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Creates a new user",
        Description = "Uses ACL to create an user with the profile Information",
        OperationId = "CreateUser"
    )]
    [SwaggerResponse(201, "The user was created", typeof(CreateUserResource))]
    public async Task<IActionResult> CreateNewUser([FromBody] CreateUserResource resource)
    {
        try
        {
            Console.WriteLine($"Request: {resource}");
            var command = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
            var user = await userCommandService.Handle(command);
            if (user == null) return BadRequest("Error creating user");
            var userResource = CreateGetUserResourceFromEntityAssembler.ToResourceFromEntity(user);
            return Ok(new { status_code = 202, message = "User created", data = userResource });

        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }




}