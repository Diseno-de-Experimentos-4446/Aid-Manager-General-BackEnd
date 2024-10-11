using System.ComponentModel.DataAnnotations.Schema;
using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.Collaborate.Domain.Model.ValueObjects;

namespace AidManager.API.Collaborate.Domain.Model.Entities;

public class Post
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    
    public string Subject { get; private set; }
    public string Description { get; private set; }
    
    public List<PostImage> ImageUrl { get; set; }

    public List<Comments> Comments { get; set; }
    public DateTime CreatedAt { get; private set; }
    public int Rating { get; set; } = 0;// don't use private set here, because we want to update it
    
    public int CompanyId { get; private set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; private set; }
    
    public string Username { get; private set; }
    
    public string UserEmail { get; private set; }
    
    public string UserImage { get; private set; }
    
    public Post()
    {
        ImageUrl = new List<PostImage>(); // Initialize the list
        Comments = new List<Comments>();
    }
    
    public Post(CreatePostCommand command, string username, string userEmail, string userImage)
    {

        this.ImageUrl = command.Images?.ConvertAll(url => new PostImage() { PostImageUrl = url }) ?? new List<PostImage>();
        this.Comments = new List<Comments>();
        this.Title = command.Title;
        this.Subject = command.Subject;
        this.Description = command.Description;
        CreatedAt = DateTime.Now;
        this.CompanyId = command.CompanyId;
        this.UserId = command.UserId;
        this.Username = username;
        this.UserEmail = userEmail;
        this.UserImage = userImage;
    }
    
    public void AddComment(AddCommentCommand command,
        string username,
        string userEmail,
        string userImage)
    {
        this.Comments.Add(new Comments(command.UserId,
            username,
            command.Comment,
            command.PostId,
            userImage,
            userEmail ));
    }
    
    public void DeleteComment(int commentId)
    {
        var comment = Comments.FirstOrDefault(c => c.Id == commentId);
        if (comment != null)
        {
            Comments.Remove(comment);
        }
    }
    
    public void UpdateRating()
    {
        this.Rating = Rating + 1;
    }

    
    
}