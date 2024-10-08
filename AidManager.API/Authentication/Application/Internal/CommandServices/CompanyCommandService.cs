using AidManager.API.Authentication.Domain.Model.Aggregates;
using AidManager.API.Authentication.Domain.Model.Commands;
using AidManager.API.Authentication.Domain.Repositories;
using AidManager.API.Authentication.Domain.Services;

namespace AidManager.API.Authentication.Application.Internal.CommandServices;

public class CompanyCommandService(ICompanyRepository companyRepository) : ICompanyCommandService
{
    public async Task<bool> Handle(CreateCompanyCommand command)
    {
        var company = new Company(command);
        
        var result = await companyRepository.CreateCompany(company);
        return result;
    }

    public async Task<Company?> Handle(ValidateRegisterCode command)
    {
        
        var company = await companyRepository.FindCompanyByRegisterCode(command.TeamRegisterCode);
        if (company == null)
        {
            throw new Exception("Not Valid Register Code");
        }
        return company;

    }

}