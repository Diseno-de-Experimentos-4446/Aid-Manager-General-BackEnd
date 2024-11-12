using System.Collections.Generic;
using System.Threading.Tasks;
using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.Collaborate.Domain.Model.Queries;
using AidManager.API.Collaborate.Domain.Model.ValueObjects;

namespace AidManager.API.Collaborate.Domain.Services;

public interface IPostQueryService
{
    // "Task" keywords it used to represent an asynchronous operation that returns a result of type "IEnumerable<Post>?"
    
    Task<IEnumerable<(Post?,User)>?> Handle(GetPostByAuthor query);
    
    Task<(Post?,User)> Handle(GetPostById query);
    
    // "Task" keywords it used to represent an asynchronous operation that returns a result of type "IEnumerable<Post>?"
    Task<IEnumerable<(Post?,User)>?> Handle(GetAllPostsByCompanyId query);
    
    Task<IEnumerable<(Post?,User)>?> Handle(GetLikedPostsByUserid query);
    
    
}