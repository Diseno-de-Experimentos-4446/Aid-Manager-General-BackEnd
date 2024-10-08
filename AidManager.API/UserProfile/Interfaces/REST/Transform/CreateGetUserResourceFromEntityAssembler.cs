using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Authentication.Interfaces.REST.Resources;

namespace AidManager.API.Authentication.Interfaces.REST.Transform;

public static class CreateGetUserResourceFromEntityAssembler
{
    public static GetUserResource ToResourceFromEntity(User user)
    {
        var role = "TeamMember";
        
        if (user.Role == 0)
        {
            role = "Manager";
        }
        
        return new GetUserResource(user.Id, user.FirstName + " " + user.LastName, user.Age, user.Email, user.Phone, user.Password, user.ProfileImg ,role, user.CompanyName);
    }
}