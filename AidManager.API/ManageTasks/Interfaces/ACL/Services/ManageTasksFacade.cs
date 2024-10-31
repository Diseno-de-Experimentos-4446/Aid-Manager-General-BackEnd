using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Queries;
using AidManager.API.ManageTasks.Domain.Services;

namespace AidManager.API.ManageTasks.Interfaces.ACL.Services;

public class ManageTasksFacade(IProjectQueryService queryService) : IManageTasksFacade
{
    public async Task<IEnumerable<Project>> GetProjectsByCompany(int companyId)
    {
        var getAllProjects = new GetAllProjectsQuery(companyId);
        return await queryService.Handle(getAllProjects);}
}