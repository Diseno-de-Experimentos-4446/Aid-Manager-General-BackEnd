using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.ManageCosts.Domain.Model.Aggregates;
using AidManager.API.ManageCosts.Interfaces.ACL;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Domain.Model.Commands;
using AidManager.API.ManageTasks.Domain.Model.Queries;
using AidManager.API.ManageTasks.Domain.Services;
using AidManager.API.UserProfile.Interfaces.ACL;

namespace AidManager.API.ManageTasks.Application.Internal.OutboundServices.ACL;

public class ExternalUserService(IUserAccountFacade accountFacade, IManageCostsFacade manageCostsFacade, IProjectQueryService projectFacade)
{
    public async Task<User> GetUserById(int id)
    {
        try
        {
            var name = await accountFacade.GetUserById(id);
            return name;
        }
        catch (Exception e)
        {
            throw new Exception("Error while getting user fullname", e);
        }
    }
    
    public async Task<Analytics> CreateAnalytics(int projectId)
    {
        return await manageCostsFacade.CreateAnalytics(projectId);
    }
    
    public async Task<IEnumerable<Project>> GetProjectsByCompany(int companyId)
    {
        var query = new GetAllProjectsQuery(companyId);
        return await projectFacade.Handle(query);
    }
    
}