using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Shared.Domain.Repositories;

namespace AidManager.API.Authentication.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindUserByEmail(string email);
    
    Task<User?> FindUserById(int id);
}