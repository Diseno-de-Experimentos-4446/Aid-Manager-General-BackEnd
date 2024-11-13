using AidManager.API.Authentication.Interfaces.REST.Resources;
using AidManager.API.Collaborate.Domain.Model.ValueObjects;

namespace AidManager.API.Collaborate.Interfaces.REST.Resources;

public record PostResource(int Id, string Title,
    string Subject,
    string Description,
    DateTime PostTime,
    int CompanyId,
    int UserId,
    string AuthorName,
    string AuthorImage,
    string AuthorEmail,
    int Rating,
    List<string> Images,
    List<CommentResource> CommentsList);