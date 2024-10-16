using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Authentication.Domain.Model.Queries;
using AidManager.API.Authentication.Domain.Repositories;
using AidManager.API.Authentication.Domain.Services;

namespace AidManager.API.Authentication.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>?> Handle(GetAllUsersByCompanyIdQuery query)
    {
        return await userRepository.FindUsersByCompanyId(query.CompanyId);
    }

    public async Task<User?> FindUserById(GetUserByIdQuery query)
    {
        return await userRepository.FindUserById(query.Id);
    }

    public async Task<User?> FindUserByEmail(GetUserByEmailQuery query)
    {
        Console.WriteLine("searching by email user");
        return await userRepository.FindUserByEmail(query.email);
    }
}