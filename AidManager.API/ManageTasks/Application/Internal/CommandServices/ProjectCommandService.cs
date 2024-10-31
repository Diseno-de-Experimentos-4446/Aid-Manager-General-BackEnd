using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.ManageTasks.Application.Internal.OutboundServices.ACL;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Model.ValueObjects;
using AidManager.API.ManageTasks.Domain.Repositories;
using AidManager.API.ManageTasks.Domain.Services;
using AidManager.API.Shared.Domain.Repositories;

namespace AidManager.API.ManageTasks.Application.Internal.CommandServices;

public class ProjectCommandService(IProjectRepository projectRepository, IUnitOfWork unitOfWork, ExternalUserService externalUserService): IProjectCommandService
{
    public async Task<Project> Handle(CreateProjectCommand command)
    {
        try {
            
            var existsByName = await projectRepository.ExistsByName(command.Name);
            
            if (existsByName)
            {
                throw new Exception($"Project with name {command.Name} already exists.");
            }
            
            var project = new Project(command);
            
                await projectRepository.AddAsync(project);
                Console.WriteLine("Project added: " + project.Id);
                await externalUserService.CreateAnalytics(project.Id);
                await unitOfWork.CompleteAsync();
                return project;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<Project> Handle(AddProjectImageCommand command)
    {
        var project = await projectRepository.GetProjectById(command.ProjectId);
       
        project.AddImage(command);
        await projectRepository.Update(project);
        return project;
        
    }

    public async Task<Project> Handle(AddTeamMemberCommand command)
    {
        try
        {
            var project = await projectRepository.GetProjectById(command.ProjectId); 
            var user = await externalUserService.GetUserById(command.UserId);

            if (project.TeamMembers.All(tm => tm.Id != user.Id))
        { 
            project.AddTeamMember(user);
            await projectRepository.Update(project);
            await unitOfWork.CompleteAsync();
        }
            
            
            await unitOfWork.CompleteAsync();
            return project;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Project> Handle(DeleteProjectCommand command)
    {
        var project = await projectRepository.GetProjectById(command.ProjectId);
        await projectRepository.Remove(project);
        await unitOfWork.CompleteAsync();
        return project;
    }

    public async Task<Project> Handle(UpdateProjectCommand command)
    {
        var project = await projectRepository.GetProjectById(command.ProjectId);
        project.UpdateProject(command);
        await projectRepository.Update(project);
        await unitOfWork.CompleteAsync();
        return project;
    }
}