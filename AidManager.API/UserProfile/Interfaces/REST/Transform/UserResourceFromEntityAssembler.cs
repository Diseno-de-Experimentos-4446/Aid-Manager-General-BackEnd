using AidManager.API.Authentication.Domain.Model.Aggregates;
using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Authentication.Interfaces.REST.Resources;
using AidManager.API.UserProfile.Interfaces.REST.Resources;

namespace AidManager.API.Authentication.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static GetUserResource ToResourceFromEntity(User user)
    {
        var role = "TeamMember";

        if (user.Role == 0)
        {
            role = "Manager";
        }
        
        return new GetUserResource(user.Id, user.FirstName + " " + user.LastName, user.Age, user.Email, user.Phone, user.Password, user.ProfileImg ,role, user.CompanyId ,user.CompanyName);
    }
    
    public static ManagerUserResource ToManagerResourceFromEntity(User user, Company company)
    {
        var role = "Manager";
        

        return new ManagerUserResource(user.Id, user.FirstName + " " + user.LastName, user.Age, user.Email, user.Phone, user.Password, user.ProfileImg, role,user.CompanyId, user.CompanyName, company.Email, company.Country, company.TeamRegisterCode);
    }
    
}