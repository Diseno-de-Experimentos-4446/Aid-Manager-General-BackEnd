using AidManager.API.ManageTasks.Application.Internal.OutboundServices.ACL;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Repositories;
using AidManager.API.ManageTasks.Domain.Services;
using AidManager.API.Shared.Domain.Repositories;

namespace AidManager.API.ManageTasks.Application.Internal.CommandServices;

public class TaskCommandService(ITaskRepository taskRepository, IUnitOfWork unitOfWork, IProjectRepository projectRepository, ExternalUserService externalUserService, IProjectCommandService projectCommandService) : ITaskCommandService
{
    public async Task<TaskItem> Handle(CreateTaskCommand command)
    {
        
        bool exists = await projectRepository.ExistsProject(command.ProjectId);

        if (!exists)
        {
            throw new Exception($"Project with id {command.ProjectId} does not exist.");
        }
        
        var user = await externalUserService.GetUserById(command.AssigneeId);
        var fullname = user.FirstName + " " + user.LastName;
        
        var task = new TaskItem(command, fullname);
        
        await projectCommandService.Handle(new AddTeamMemberCommand(command.AssigneeId, command.ProjectId));
        
        try
        {
            await taskRepository.AddAsync(task);
            await unitOfWork.CompleteAsync();
            return task;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    public async Task<TaskItem> Handle(UpdateTaskCommand command)
    {
        
        bool exists = await projectRepository.ExistsProject(command.ProjectId);

        if (!exists)
        {
            throw new Exception($"Project with id {command.ProjectId} does not exist.");
        }
        
        var username = await externalUserService.GetUserById(command.AssigneeId);
        var task = await taskRepository.GetTaskById(command.Id);
        var fullname = username.FirstName + " " + username.LastName;
        

        if (task is null) throw new Exception("Task not found");
        
        task.UpdateTask(command, fullname);
        await taskRepository.Update(task);
        await unitOfWork.CompleteAsync();
        return task;
    }
    
    public async Task<TaskItem> Handle(DeleteTaskCommand command)
    {
        
        bool exists = await projectRepository.ExistsProject(command.ProjectId);

        if (!exists)
        {
            throw new Exception($"Project with id {command.ProjectId} does not exist.");
        }
        
        var task = await taskRepository.GetTaskById(command.Id);
        if (task is null) throw new Exception("Task not found");
        await taskRepository.Remove(task);
        await unitOfWork.CompleteAsync();
        return task;
    }
    
}