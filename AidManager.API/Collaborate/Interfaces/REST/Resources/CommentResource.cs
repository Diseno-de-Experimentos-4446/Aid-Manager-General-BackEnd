namespace AidManager.API.Collaborate.Interfaces.REST.Resources;

public record CommentResource
(int Id, string Comment, int UserId, string AuthorName, string AuthorEmail ,string AuthorImage, int PostId, string CommentTime);