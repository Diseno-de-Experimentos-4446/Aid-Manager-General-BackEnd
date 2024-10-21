using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Model.ValueObjects;

namespace AidManager.API.ManageTasks.Domain.Services;

public interface IProjectCommandService
{
    Task<Project> Handle(CreateProjectCommand command);
    
    Task<Project> Handle(AddProjectImageCommand command);

    Task<Project> Handle(AddTeamMemberCommand command);
    
    Task<Project> Handle(DeleteProjectCommand command);
    
    Task<Project> Handle(UpdateProjectCommand command);

}