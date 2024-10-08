using AidManager.API.Authentication.Application.Internal.CommandServices;
using AidManager.API.Authentication.Domain.Model.Commands;
using AidManager.API.Authentication.Interfaces.REST.Resources;

namespace AidManager.API.Authentication.Interfaces.REST.Transform;

public static class CreateUserCredentialsCommandFromResourceAssembler
{
    public static ValidateUserCredentialsCommand ToCommandFromResource(UserCredentialsResource resource)
    {
        return new ValidateUserCredentialsCommand(resource.Email, resource.Password, resource.CompanyRegisterCode);
    }
}