﻿using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.ManageTasks.Application.Internal.OutboundServices;
using AidManager.API.ManageTasks.Application.Internal.OutboundServices.ACL;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Repositories;
using AidManager.API.ManageTasks.Domain.Services;
using AidManager.API.Shared.Domain.Repositories;

namespace AidManager.API.ManageTasks.Application.Internal.CommandServices;

public class TaskCommandService(ITaskEventHandlerService eventHandlerService,ITaskRepository taskRepository, IUnitOfWork unitOfWork, IProjectRepository projectRepository, ExternalUserService externalUserService) : ITaskCommandService
{
    public async Task<(TaskItem, User)> Handle(CreateTaskCommand command)
    {
        try
        {
            bool exists = await projectRepository.ExistsProject(command.ProjectId);

            if (!exists)
            {
                throw new Exception($"Project with id {command.ProjectId} does not exist.");
            }
            
            var user = await externalUserService.GetUserById(command.AssigneeId);
            if (user is null) throw new Exception("User not found");
            
            if (user.Role == 0)
            {
                throw new Exception($"Cant assign task to a manager user.");
            }
            
            var task = new TaskItem(command);
            
            await eventHandlerService.HandleAddTeamMember(new AddTeamMemberCommand(command.AssigneeId, command.ProjectId));
            await taskRepository.AddAsync(task);
            await unitOfWork.CompleteAsync();
            return (task, user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    public async Task<(TaskItem, User)> Handle(UpdateTaskCommand command)
    {
        
        bool exists = await projectRepository.ExistsProject(command.ProjectId);

        if (!exists)
        {
            throw new Exception($"Project with id {command.ProjectId} does not exist.");
        }
        var user = await externalUserService.GetUserById(command.AssigneeId);
        if (user is null) throw new Exception("User not found");
        
        var task = await taskRepository.GetTaskById(command.Id);

        if (task is null) throw new Exception("Task not found");
        
        task.UpdateTask(command);
        await taskRepository.Update(task);
        await unitOfWork.CompleteAsync();
        return (task, user);
    }
    
    public async Task<(TaskItem,User)> Handle(DeleteTaskCommand command)
    {
        
        bool exists = await projectRepository.ExistsProject(command.ProjectId);
        

        if (!exists)
        {
            throw new Exception($"Project with id {command.ProjectId} does not exist.");
        }
        
        var task = await taskRepository.GetTaskById(command.Id);
        if (task is null) throw new Exception("Task not found");
        var user = await externalUserService.GetUserById(task.AssigneeId);
        if (user is null) throw new Exception("User not found");
        await taskRepository.Remove(task);
        await unitOfWork.CompleteAsync();
        return (task, user);
    }
    
}