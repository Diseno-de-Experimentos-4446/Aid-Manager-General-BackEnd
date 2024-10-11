using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.UserProfile.Interfaces.ACL;

namespace AidManager.API.ManageTasks.Application.Internal.OutboundServices.ACL;

public class ExternalUserService(IUserAccountFacade accountFacade)
{
    public async Task<string> GetUserFullnameById(int id)
    {
        try
        {
            var name = await accountFacade.GetUserFullnameById(id);
            return name;
        }
        catch (Exception e)
        {
            throw new Exception("Error while getting user fullname", e);
        }
    }
    
}