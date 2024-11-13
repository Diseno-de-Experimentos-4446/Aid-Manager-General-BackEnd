using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Authentication.Domain.Model.Queries;
using AidManager.API.Authentication.Domain.Repositories;
using AidManager.API.Authentication.Domain.Services;

namespace AidManager.API.Authentication.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>?> Handle(GetAllUsersByCompanyIdQuery query)
    {
        var users =  await userRepository.FindUsersByCompanyId(query.CompanyId);
        var userList = new List<User>();
        
        foreach (var user in users)
        {
            if (user is { FirstName: "Deleted", Age: 0 })
            {
                continue;
            }
            userList.Add(user);
        }

        return userList;
    }

    public async Task<User?> FindUserById(GetUserByIdQuery query)
    {
        return await userRepository.FindUserById(query.Id);
    }

    public async Task<User?> FindUserByEmail(GetUserByEmailQuery query) //no lo uso pero en caso de usarse validar
    {
        Console.WriteLine("searching by email user");
        return await userRepository.FindUserByEmail(query.email);
    }
}