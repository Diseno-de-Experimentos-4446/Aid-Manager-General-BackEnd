using AidManager.API.Authentication.Interfaces.REST.Resources;
using AidManager.API.Authentication.Interfaces.REST.Transform;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.Collaborate.Interfaces.REST.Resources;

namespace AidManager.API.Collaborate.Interfaces.REST.Transform;

public static class PostResourceFromEntityAssembler
{
    public static PostResource ToResourceFromEntity(Post post)
    {
        
        return new PostResource(
            post.Id,
            post.Title,
            post.Subject,
            post.Description, 
            post.CreatedAt,
            post.CompanyId,
            post.UserId,
            post.Username,
            post.UserEmail,
            post.UserImage,
            post.Rating,
            post.ImageUrl.Select(img => img.PostImageUrl).ToList(),
            post.Comments
        );
    }
}