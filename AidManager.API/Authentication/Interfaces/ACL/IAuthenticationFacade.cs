using AidManager.API.Authentication.Domain.Model.Aggregates;
using AidManager.API.Authentication.Domain.Services;

namespace AidManager.API.Authentication.Interfaces.ACL;

public interface IAuthenticationFacade
{
    Task<bool> CreateCompany(string companyName, string country, string email, int userId);
    Task<Company?> ValidateRegisterCode(string TeamRegisterCode);
    Task<Company?> GetCompanyByManagerId(int Id);
}