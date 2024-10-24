using System.Net.Mime;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Model.Queries;
using AidManager.API.ManageTasks.Domain.Services;
using AidManager.API.ManageTasks.Interfaces.REST.Resources;
using AidManager.API.ManageTasks.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AidManager.API.ManageTasks.Interfaces;

[ApiController]
[Route("api/v1/Projects/{projectId}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class TaskItemsController(ITaskCommandService taskCommandService, ITaskQueryService taskQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a Task",
        Description = "Create a Project Task",
        OperationId = "CreateTaskItem"
    )]
    [SwaggerResponse(201, "Task Created", typeof(CreateTaskItemResource))]
    public async Task<ActionResult> CreateTaskItem(int projectId, [FromBody] CreateTaskItemResource resource)
    {
        try
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
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
        
        
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a Task by Id",
        Description = "Get Task Item by using its ID",
        OperationId = "GetTask"
    )]
    public async Task<ActionResult<TaskItemResource>> GetTaskItemById(int id)
    {
        try
        {
            var taskItem = await taskQueryService.Handle(new GetTaskByIdQuery(id));
            var resource = TaskItemResourceFromEntityAssembler.ToResourceFromEntity(taskItem);
            return Ok(resource);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
        
    }
    
    
    [HttpPatch("{id}")]
    [SwaggerOperation(
        Summary = "Change Status",
        Description = "Update a Status Project Task",
        OperationId = "UpdateStatusTaskItem"
    )]
    [SwaggerResponse(201, "Task Updated", typeof(UpdateTaskItemResource))]
    public async Task<ActionResult> ChangeStatusTaskItem(int id, int projectId, [FromBody] UpdateProjectStatusResource request)
    {
        try
        {
            var getTaskByIdQuery = new GetTaskByIdQuery(id);
            var taskItem = await taskQueryService.Handle(getTaskByIdQuery);
            var resource = new UpdateTaskItemResource(taskItem.Title, taskItem.Description, taskItem.DueDate, request.status, taskItem.AssigneeId);

            var updateTaskCommand = UpdateTaskItemCommandFromResourceAssembler.ToCommandFromResource(resource, id, projectId);
            var result = await taskCommandService.Handle(updateTaskCommand);
            return Ok(TaskItemResourceFromEntityAssembler.ToResourceFromEntity(result));
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
    
    [HttpPatch("edit/{id}")]
    [SwaggerOperation(
        Summary = "Update a Task",
        Description = "Update a Project Task",
        OperationId = "UpdateTaskItem"
    )]
    [SwaggerResponse(201, "Task Updated", typeof(UpdateTaskItemResource))]

    public async Task<ActionResult> UpdateTaskItem( int id ,int projectId,[FromBody] UpdateTaskItemResource resource)
    {
        try
        { 
            var updateTaskCommand = UpdateTaskItemCommandFromResourceAssembler.ToCommandFromResource(resource, id, projectId);
            var result = await taskCommandService.Handle(updateTaskCommand);
            return Ok(TaskItemResourceFromEntityAssembler.ToResourceFromEntity(result));
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);

        }
       
    }
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Deletes a Task",
        Description = "Delete a ProjectTask",
        OperationId = "DeleteTaskItem"
    )]
    public async Task<ActionResult> DeleteTaskItem(int id, int projectId)
    {
        try
        {
            var deleteTaskItemCommand = new DeleteTaskCommand(id, projectId);
            var result = await taskCommandService.Handle(deleteTaskItemCommand);
            return Ok(TaskItemResourceFromEntityAssembler.ToResourceFromEntity(result));
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);

        }
       
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Tasks",
        Description = "Get all tasks from a project",
        OperationId = "GetAllTasks"
    )]
    public async Task<ActionResult<IEnumerable<TaskItemResource>>> GetAllTaskItems(int projectId)
    {
        try
        {
            var getAllTasksQueryByProjectId = new GetTasksByProjectIdQuery(projectId); 
            var result = await taskQueryService.Handle(getAllTasksQueryByProjectId);
            var resources = result.Select(TaskItemResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);

        }
        
    }
    
}