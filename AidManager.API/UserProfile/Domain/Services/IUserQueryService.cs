using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Authentication.Domain.Model.Queries;

namespace AidManager.API.Authentication.Domain.Services;

public interface IUserQueryService
{
    Task<IEnumerable<User>?> Handle(GetAllUsersQuery query);
    Task<User?> FindUserByEmail(GetUserByEmailQuery email);
}