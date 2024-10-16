using System.Net.Mime;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Model.Queries;
using AidManager.API.ManageTasks.Domain.Services;
using AidManager.API.ManageTasks.Interfaces.REST.Resources;
using AidManager.API.ManageTasks.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace AidManager.API.ManageTasks.Interfaces;

[ApiController]
[Route("api/v1/Projects/{projectId}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class TaskItemsController(ITaskCommandService taskCommandService, ITaskQueryService taskQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateTaskItem(int projectId, [FromBody] CreateTaskItemResource resource)
    {
        var createTaskItemCommand = CreateTaskItemCommandFromResourceAssembler.ToCommandFromResource(resource, projectId); 
        Console.WriteLine("The TaskItemController is called. and the Task Item is assembled." + createTaskItemCommand);
        var result = await taskCommandService.Handle(createTaskItemCommand);
        Console.WriteLine("The Create Item Command is handled in the taskCommandService.");
        var taskItemById = GetTaskItemById(result.Id);
        Console.WriteLine("Task by id called" + taskItemById.Result);
        return CreatedAtAction(nameof(GetTaskItemById), new {projectId = projectId, id = result.Id }, 
            TaskItemResourceFromEntityAssembler.ToResourceFromEntity(result));
        
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItemResource>> GetTaskItemById(int id)
    {
        var taskItem = await taskQueryService.Handle(new GetTaskByIdQuery(id));
        var resource = TaskItemResourceFromEntityAssembler.ToResourceFromEntity(taskItem);
        return Ok(resource);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateTaskItem( int id ,int projectId,[FromBody] UpdateTaskItemResource resource)
    {
        var updateTaskCommand = UpdateTaskItemCommandFromResourceAssembler.ToCommandFromResource(resource, id, projectId);
        var result = await taskCommandService.Handle(updateTaskCommand);
        return Ok(TaskItemResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTaskItem(int id, int projectId)
    {
        var deleteTaskItemCommand = new DeleteTaskCommand(id, projectId);
        var result = await taskCommandService.Handle(deleteTaskItemCommand);
        return Ok(TaskItemResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemResource>>> GetAllTaskItems(int projectId)
    {
        var getAllTasksQueryByProjectId = new GetTasksByProjectIdQuery(projectId);
        var result = await taskQueryService.Handle(getAllTasksQueryByProjectId);
        var resources = result.Select(TaskItemResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
}