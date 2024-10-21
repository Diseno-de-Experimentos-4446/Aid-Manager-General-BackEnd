using System.Net.Mime;
using AidManager.API.Authentication.Domain.Model.Commands;
using AidManager.API.Authentication.Domain.Model.Queries;
using AidManager.API.Authentication.Domain.Services;
using AidManager.API.Authentication.Interfaces.REST.Resources;
using AidManager.API.Authentication.Interfaces.REST.Transform;
using AidManager.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using AidManager.API.UserManagement.UserProfile.Application.Internal.OutboundServices.ACL;
using AidManager.API.UserProfile.Interfaces.REST.Resources;
using AidManager.API.UserProfile.Interfaces.REST.Transform;

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AidManager.API.Authentication.Interfaces;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UsersController(IUserCommandService userCommandService, IUserQueryService userQueryService, ExternalUserAuthService externalUserAuthService) : ControllerBase
{
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
            if (user.Role == 0 )
            {            
                var company = await externalUserAuthService.FetchCompanyByUserId(user.Id);
                 var managerResource = UserResourceFromEntityAssembler.ToManagerResourceFromEntity(user, company);
                 return Ok(new { status_code = 202, message = "Manager User created", data = managerResource });

            }
            
            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
            return Ok(new { status_code = 202, message = "User created", data = userResource });

            
            

        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
    
    [HttpGet("{companyId}")]
    [SwaggerOperation(
        Summary = "Obtains all users",
        Description = "Obtains all users",
        OperationId = "GetAllUsers"
    )]
    [SwaggerResponse(200, "The users were obtained")]
    public async Task<IActionResult> GetAllUsers(int companyId)
    {
        try
        {
            var query = new GetAllUsersByCompanyIdQuery(companyId);
            var users = await userQueryService.Handle(query); 
            if(users == null) return BadRequest();
            var usersResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(usersResources);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    [HttpGet("user/{id}")]
    [SwaggerOperation(
        Summary = "Obtains a user by id",
        Description = "Obtains a user by id",
        OperationId = "GetUserById"
    )]
    [SwaggerResponse(200, "The user was obtained")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var query = new GetUserByIdQuery(id);
            var user = await userQueryService.FindUserById(query);
            if(user == null) return BadRequest();

            if (user.Role == 0)
            {
                var company = await externalUserAuthService.FetchCompanyByUserId(user.Id);
                var managerResource = UserResourceFromEntityAssembler.ToManagerResourceFromEntity(user, company);
                return Ok(managerResource);
            }
            
            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
            return Ok(userResource);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    [HttpPut("{userId}")]
    [SwaggerOperation(
        Summary = "Update a User",
        Description = "Update User Profile",
        OperationId = "UpdateUser"
    )]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserResource resource)
    {
        try
        {
            var command = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
            var updatedUser = await userCommandService.Handle(command, userId);
            if(updatedUser == null) return BadRequest();
        
            var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(updatedUser);
            return Ok(userResource);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    
    [HttpDelete("kick-member/{userId}")]
    [SwaggerOperation(
        Summary = "Kick User",
        Description = "Delete User",
        OperationId = "DeleteUser"
    )]
    public async Task<IActionResult> KickUserByCompanyId(int userId)
    {
        try
        {
            if (!await userCommandService.Handle(new KickUserByCompanyIdCommand(userId)))
                return Ok(new { status_code = 404, message = "User not found"});
            return Ok(new {status_code=202, message = "User kicked"});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }


    




}