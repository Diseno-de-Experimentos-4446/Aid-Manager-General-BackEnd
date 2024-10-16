namespace AidManager.API.Collaborate.Domain.Model.ValueObjects;

public class Comments(int userId, string authorName, string comment, int postId, string authorImage, string authorEmail)
{
    public int Id { get; set; }
    public int UserId { get; set; } = userId;

    public string AuthorImage { get; set; } = authorImage;
    
    public string AuthorEmail { get; set; } = authorEmail;
    public string AuthorName { get; set; } = authorName;
    public string Comment { get; set; } = comment;
    public int PostId { get; set; } = postId;
    public DateTime TimeOfComment { get; set; } = DateTime.Now;
}