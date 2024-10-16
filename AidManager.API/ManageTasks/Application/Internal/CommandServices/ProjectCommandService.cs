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
        bool existsByName = await projectRepository.ExistsByName(command.Name);
        
        if (existsByName)
        {
            throw new Exception($"Project with name {command.Name} already exists.");
        }
        
        var project = new Project(command);
        
        try
        {
            await projectRepository.AddAsync(project);
            await unitOfWork.CompleteAsync();
            return project;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<List<ProjectImage?>> Handle(AddProjectImageCommand command)
    {
        var project = await projectRepository.GetProjectById(command.ProjectId);
       
        project.AddImage(command);
        await projectRepository.Update(project);
        return project.ImageUrl;
        
    }

    public async Task<Project> Handle(AddTeamMemberCommand command)
    {
        var project = await projectRepository.GetProjectById(command.ProjectId);
        var user = await externalUserService.GetUserById(command.UserId);
        
        project.AddTeamMember(user);
        
        try
        {
            await projectRepository.Update(project);
            await unitOfWork.CompleteAsync();
            return project;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}