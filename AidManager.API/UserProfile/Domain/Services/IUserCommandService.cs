using AidManager.API.Authentication.Domain.Model.Commands;
using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.UserProfile.Domain.Model.Commands;

namespace AidManager.API.Authentication.Domain.Services;

public interface IUserCommandService
{
    Task<User?> Handle(CreateUserCommand command);
    Task<User> Handle(UpdateUserCommand command, string email);
    Task<bool> AuthenticateUser(ValidateUserCredentialsCommand command);
    Task<bool> Handle(EditCompanyIdCommand command, int companyId);
    Task<bool> Handle(KickUserByCompanyIdCommand command);
    Task<User?> Handle(UpdateUserCompanyNameCommand command);
}