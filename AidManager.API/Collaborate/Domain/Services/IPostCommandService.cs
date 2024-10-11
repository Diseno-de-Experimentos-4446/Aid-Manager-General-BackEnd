using System.Threading.Tasks;
using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.Collaborate.Domain.Model.ValueObjects;

namespace AidManager.API.Collaborate.Domain.Services;

public interface IPostCommandService
{
    Task<Post?> Handle(CreatePostCommand command);
    Task<Post?> Handle(DeletePostCommand command);
    
    Task<Comments?> Handle(AddCommentCommand command);
    Task<Post?> Handle(UpdatePostRatingCommand command);
    
    
    Task<PostImage?> Handle(DeletePostImageCommand command);
    
    Task<Comments?> Handle(DeleteCommentCommand command);
    
}