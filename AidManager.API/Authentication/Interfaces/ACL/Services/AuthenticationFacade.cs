using AidManager.API.Authentication.Domain.Model.Aggregates;
using AidManager.API.Authentication.Domain.Model.Commands;
using AidManager.API.Authentication.Domain.Model.Queries;
using AidManager.API.Authentication.Domain.Services;

namespace AidManager.API.Authentication.Interfaces.ACL.Services;

public class AuthenticationFacade(ICompanyCommandService commandService, ICompanyQueryService queryService) : IAuthenticationFacade
{
    public async Task<bool> CreateCompany(string companyName, string country, string email, int userId)
    {
        var createCompanyCommand = new CreateCompanyCommand(companyName, country, email, userId);
        return await commandService.Handle(createCompanyCommand);
    }

    public async Task<Company?> ValidateRegisterCode(string TeamRegisterCode)
    {
        var validateRegisterCodeCommand = new ValidateRegisterCode(TeamRegisterCode);
        return await commandService.Handle(validateRegisterCodeCommand);
    }

    public async Task<Company?> GetCompanyByManagerId(int userId)
    {
        var getCompanyByUserId = new GetCompanyByUserIdQuery(userId);
        return await queryService.Handle(getCompanyByUserId);
    }
}