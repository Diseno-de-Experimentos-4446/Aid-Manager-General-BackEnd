using System.Collections.Generic;
using System.Threading.Tasks;
using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.Collaborate.Domain.Model.Queries;
using AidManager.API.Collaborate.Domain.Model.ValueObjects;

namespace AidManager.API.Collaborate.Domain.Services;

public interface IPostQueryService
{
    // "Task" keywords it used to represent an asynchronous operation that returns a result of type "IEnumerable<Post>?"
    Task<List<Comments>?> Handle(GetCommentsByPostIdQuery query);
    
    Task<IEnumerable<Post>?> Handle(GetPostByAuthor query);
    
    Task<Post?> Handle(GetPostById query);
    
    // "Task" keywords it used to represent an asynchronous operation that returns a result of type "IEnumerable<Post>?"
    Task<IEnumerable<Post>?> Handle(GetAllPostsByCompanyId query);
}