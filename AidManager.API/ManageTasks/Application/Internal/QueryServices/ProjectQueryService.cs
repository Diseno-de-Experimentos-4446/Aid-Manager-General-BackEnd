using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.ManageTasks.Application.Internal.OutboundServices.ACL;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Model.Queries;
using AidManager.API.ManageTasks.Domain.Repositories;
using AidManager.API.ManageTasks.Domain.Services;

namespace AidManager.API.ManageTasks.Application.Internal.QueryServices;

public class ProjectQueryService(IFavoriteProjects favoriteProjects,IProjectRepository projectRepository, ExternalUserService externalUserService) :IProjectQueryService
{
    public async Task<IEnumerable<(Project,List<User>)>> Handle(GetAllProjectsQuery query)
    {
        
       var projectList = await projectRepository.GetProjectsByCompanyId(query.CompanyId);
       var projectUserList = new List<(Project, List<User>)>();
       
       
         foreach (var project in projectList)
         {
             var team = new List<User>();
              foreach (var teamMember in project.TeamMembers)
              {
                var user = await externalUserService.GetUserById(teamMember.Id);
                team.Add(user);
              }
              projectUserList.Add((project, team));
              
         }
       
         return projectUserList;
       
    }
    
    public async Task<(Project, List<User>)> Handle(GetProjectByIdQuery query)
    {
        var project = await projectRepository.GetProjectById(query.id);
        var team = new List<User>();
        
        foreach (var teamMember in project.TeamMembers)
        {
            var user = await externalUserService.GetUserById(teamMember.Id);
            team.Add(user);
        }
        
        
        return (project, team);
        
    }

    public async Task<IEnumerable<User>> Handle(GetAllTeamMembers query)
    {
        var project = await projectRepository.GetProjectById(query.ProjectId);
        var team = new List<User>();
        
        foreach (var teamMember in project.TeamMembers)
        {
            var user = await externalUserService.GetUserById(teamMember.Id);
            team.Add(user);
        }
        
        return team;
    }

    public async Task<IEnumerable<(Project, List<User>)>> Handle(GetAllProjectsByUserIdQuery query)
    {
        var projectList = await projectRepository.GetProjectsByUserId(query.UserId);
        var projectUserList = new List<(Project, List<User>)>();
        

       
        foreach (var project in projectList)
        {
            var team = new List<User>();
            foreach (var teamMember in project.TeamMembers)
            {
                var user = await externalUserService.GetUserById(teamMember.Id);
                team.Add(user);
            }
            projectUserList.Add((project, team));
        }
       
        return projectUserList;
    }

    public async Task<IEnumerable<(Project, List<User>)>> Handle(GetFavoriteProjectsByUserId query)
    {
       var projectsList = await favoriteProjects.GetFavoriteProjectsByUserIdAsync(query.UserId);
       var projectUserList = new List<(Project, List<User>)>();
       var team = new List<User>();
       
         foreach (var project in projectsList)
         {
                var projectEntity = await projectRepository.GetProjectById(project.ProjectId);
                foreach (var teamMember in projectEntity.TeamMembers)
                {
                    var user = await externalUserService.GetUserById(teamMember.Id);
                    team.Add(user);
                }
                projectUserList.Add((projectEntity, team));
         }
         
         return projectUserList;
       
    }
}