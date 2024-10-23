using System.Net.Mime;
using AidManager.API.ManageTasks.Domain.Model.Queries;
using AidManager.API.ManageTasks.Domain.Services;
using AidManager.API.ManageTasks.Interfaces.REST.Resources;
using AidManager.API.ManageTasks.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AidManager.API.ManageTasks.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProjectsController (IProjectCommandService projectCommandService, IProjectQueryService projectQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a Project",
        Description = "Create a new Project",
        OperationId = "CreateProject"
    )]
    public async Task<IActionResult> CreateProject(CreateProjectResource resource)
    {
        try
        {
            var createProjectCommand = CreateProjectCommandFromResourceAssembler.ToCommandFromResource(resource);
            var project = await projectCommandService.Handle(createProjectCommand);
            var projectResource = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
            return Ok(projectResource);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);

        }
        
    }

    [HttpPut("{projectId}")]
    [SwaggerOperation(
        Summary = "Update project",
        Description = "Update a project",
        OperationId = "UpdateProject"
    )]
    public async Task<IActionResult> UpdateProject(int projectId,UpdateProjectResource resource)
    {
        var project = await projectCommandService.Handle(UpdateProjectCommandFromResourceAssembler.ToCommandFromResource(projectId, resource));
        var projectResource = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
        return Ok(projectResource);
    }

    
    [HttpGet("{companyId}")]
    [SwaggerOperation(
        Summary = "Get All Projects",
        Description = "Get all projects by company id",
        OperationId = "GetAllProjects"
    )]
    public async Task<IActionResult> GetAllProjects(int companyId)
    {
        var projects = await projectQueryService.Handle(new GetAllProjectsQuery(companyId));
        var projectResources = projects.Select(ProjectResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(projectResources);
    }
    
    [HttpGet("get/{projectId}")]
    [SwaggerOperation(
        Summary = "Get Project",
        Description = "Get project by Id",
        OperationId = "GetProject"
    )]
    public async Task<IActionResult> GetProject(int projectId)
    {
        var project = await projectQueryService.Handle(new GetProjectByIdQuery(projectId));
        var projectResource = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
        return Ok(projectResource);
    }
    
    [HttpPatch("{projectId}/images")]
    [SwaggerOperation(
        Summary = "Update a project Images",
        Description = "Update project images",
        OperationId = "UpdateProjectImages"
    )]
    public async Task<IActionResult> UpdateProject(AddImageResource resource)
    {
        try
        {
            var project = await projectCommandService.Handle(AddImageResourceFromResourceAssembler.ToCommandFromResource(resource));
            var projectResource = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
            return Ok(projectResource);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);

        }
        
    }
    
    [HttpGet("team/{projectId}")]
    [SwaggerOperation(
        Summary = "Get all project Team Members",
        Description = "Get all team members of a project",
        OperationId = "GetTeamMembers"
    )]
    public async Task<IActionResult> GetTeamMembers(int projectId)
    {
        var users = await projectQueryService.Handle(new GetAllTeamMembers(projectId));
        var projectResource = users.Select(TeamMemberResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(projectResource);
    }
    
    [HttpDelete("{projectId}")]
    [SwaggerOperation(
        Summary = "Delete project",
        Description = "Delete a project",
        OperationId = "DeleteProject"
    )]
    public async Task<IActionResult> DeleteProject(int projectId)
    {
        try
        {
            var project = await projectCommandService.Handle(DeleteProjectCommandFromResourceAssembler.ToCommandFromResource(projectId));
            var projectResource = ProjectResourceFromEntityAssembler.ToResourceFromEntity(project);
            return Ok(projectResource);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);

        }
        
    }
    
    
    
}