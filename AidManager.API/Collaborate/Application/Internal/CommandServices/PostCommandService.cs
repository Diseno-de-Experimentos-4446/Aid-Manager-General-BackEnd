using System.Threading.Tasks;
using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Authentication.Domain.Repositories;
using AidManager.API.Collaborate.Application.Internal.OutboundServices.ACL;
using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.Collaborate.Domain.Model.ValueObjects;
using AidManager.API.Collaborate.Domain.Repositories;
using AidManager.API.Collaborate.Domain.Services;
using AidManager.API.Collaborate.Infraestructure.Repositories;
using AidManager.API.Shared.Domain.Repositories;

namespace AidManager.API.Collaborate.Application.Internal.CommandServices;

public class PostCommandService(ExternalUserAccountService externalUserAccountService,IPostRepository postRepository, IUnitOfWork unitOfWork): IPostCommandService
{
    
    public async Task<Comments?> Handle(AddCommentCommand command)
    {
        var post = await postRepository.FindPostById(command.PostId);
        if (post == null)
        {
            return null;
        }
        
        var user = await externalUserAccountService.GetUserById(command.UserId);
        if (user == null)
        {
            return null;
        }
        var username = user.FirstName + " " + user.LastName;
        
        
        post.AddComment(command,username, user.Email, user.ProfileImg);
        await postRepository.Update(post);
        
        Console.WriteLine("Comment added: " + await postRepository.FindPostById(command.PostId));
        
        await unitOfWork.CompleteAsync();
        return post.Comments.Last();
    }
    
    public async Task<Post?> Handle(CreatePostCommand command)
    {
        var user = await externalUserAccountService.GetUserById(command.UserId);
        Console.WriteLine("==================================== \n\n\n USER: " + user, "USER IMAGE: " + user?.ProfileImg, " \n\n\n =================");
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var name = user.FirstName + " " + user?.LastName;
        var post = new Post(command, name, user.Email, user.ProfileImg);
        await postRepository.AddAsync(post);
        await unitOfWork.CompleteAsync();
        return post;
    }
    
    public async Task<Post?> Handle(DeletePostCommand command)
    {
        var post = await postRepository.FindPostById(command.Id);
        
        if (post == null)
        {
            return null;
        }
        await postRepository.Remove(post);
        await unitOfWork.CompleteAsync();
        return post;
    }
    
    public async Task<Post?> Handle(UpdatePostRatingCommand command)
    {
        var post = await postRepository.FindPostById(command.PostId);
        
        if (post == null)
        {
            return null;
        }
        
        post.UpdateRating();
        await unitOfWork.CompleteAsync();
        return post;
    }
    

    public async Task<PostImage?> Handle(DeletePostImageCommand command)
    {
        var post = await postRepository.FindPostById(command.PostId);
        if (post == null)
        {
            return null;
        }
        
        var postImage = post.ImageUrl.FirstOrDefault(i => i.Id == command.PostImageId);
        
        post.DeleteComment(command.PostImageId);
        
        await unitOfWork.CompleteAsync();
        return postImage;
    }

    public async Task<Comments?> Handle(DeleteCommentCommand command)
    {
        var post = await postRepository.FindPostById(command.PostId);
        if (post == null)
        {
            return null;
        } 
        var comment =  post.Comments.FirstOrDefault(c => c.Id == command.CommentId);
        post.DeleteComment(command.CommentId);
        await unitOfWork.CompleteAsync();
        return comment;
    }
}