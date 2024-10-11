namespace AidManager.API.Collaborate.Interfaces.REST.Resources;

public record CommentResource
(int Id, string Comment, int UserId, string UserName, string userEmail ,string userImage, int PostId, string CommentTime);