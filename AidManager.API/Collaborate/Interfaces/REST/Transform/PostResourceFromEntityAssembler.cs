using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Authentication.Interfaces.REST.Resources;
using AidManager.API.Authentication.Interfaces.REST.Transform;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.Collaborate.Interfaces.REST.Resources;

namespace AidManager.API.Collaborate.Interfaces.REST.Transform;

public static class PostResourceFromEntityAssembler
{
    public static PostResource ToResourceFromEntity(Post post, User user)
    {
        
        return new PostResource(
            post.Id,
            post.Title,
            post.Subject,
            post.Description, 
            post.CreatedAt,
            post.CompanyId,
            post.UserId,
            user.FirstName + " " + user.LastName,
            user.Email,
            user.ProfileImg,
            post.Rating,
            post.ImageUrl.Select(img => img.PostImageUrl).ToList(),
            post.Comments
        );
    }
    
    
}