using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Shared.Domain.Repositories;

namespace AidManager.API.Authentication.Domain.Repositories;

public interface IDeletedUserRepository : IBaseRepository<DeletedUser>
{
        Task<List<User>> FindUsersByCompanyId(int companyId);
        
        Task<User?> FindUserByEmail(string email);
        
        Task<User?> FindUserById(int id);
}