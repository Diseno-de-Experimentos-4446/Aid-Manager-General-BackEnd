using AidManager.API.Collaborate.Domain.Model.ValueObjects;
using AidManager.API.Collaborate.Interfaces.REST.Resources;

namespace AidManager.API.Collaborate.Interfaces.REST.Transform;

public static class CommentResourceFromEntityAssembler
{
    public static CommentResource ToResourceFromEntity(Comments comments)
    {
        return new CommentResource(
            comments.Id,
            comments.Comment,
            comments.UserId,
            comments.AuthorName,
            comments.AuthorEmail,
            comments.AuthorImage,
            comments.PostId,
            comments.TimeOfComment.ToString("yyyy-MM-dd HH:mm:ss")
        );
    }

}