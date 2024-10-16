using System.Collections;
using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Queries;
using AidManager.API.ManageTasks.Domain.Repositories;
using AidManager.API.ManageTasks.Domain.Services;

namespace AidManager.API.ManageTasks.Application.Internal.QueryServices;

public class ProjectQueryService(IProjectRepository projectRepository) :IProjectQueryService
{
    public async Task<IEnumerable<Project>> Handle(GetAllProjectsQuery query)
    {
        return await projectRepository.GetProjectsByCompanyId(query.CompanyId);
    }
    
    public async Task<Project> Handle(GetProjectByIdQuery query)
    {
        return await projectRepository.GetProjectById(query.id);
    }

    public async Task<IEnumerable<User>> Handle(GetAllTeamMembers query)
    {
        return await projectRepository.GetTeamMembers(query.ProjectId);
    }
  
}