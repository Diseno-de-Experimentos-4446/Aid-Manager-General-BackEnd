using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.Collaborate.Domain.Model.Queries;

namespace AidManager.API.Collaborate.Domain.Services;

public interface IFavoritePostQueryService
{
    Task<List<(Post, User)>> Handle(GetFavoritePosts query);
    
}