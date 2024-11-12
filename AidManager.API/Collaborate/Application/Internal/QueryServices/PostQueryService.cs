using System.Collections.Generic;
using System.Threading.Tasks;
using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.Collaborate.Application.Internal.OutboundServices.ACL;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.Collaborate.Domain.Model.Queries;
using AidManager.API.Collaborate.Domain.Model.ValueObjects;
using AidManager.API.Collaborate.Domain.Repositories;
using AidManager.API.Collaborate.Domain.Services;

namespace AidManager.API.Collaborate.Application.Internal.QueryServices;

public class PostQueryService(IPostRepository postRepository, ExternalUserAccountService externalUserAccountService, ILikedPostRepository likedPostRepository) : IPostQueryService
{
    
    public async Task<IEnumerable<(Post?, User)>?> Handle(GetPostByAuthor query)
    {
        
        var posts = await postRepository.GetPostByAuthor(query.Id);
        if (posts is null)
        {
            throw new Exception("ERROR GETTING POSTS BY AUTHOR");
        }
        
        
        var user = await externalUserAccountService.GetUserById(query.Id);
        if (user is null)
        {
            throw new Exception("ERROR GETTING USER BY ID FOR POSTS BY AUTHOR");
        }

        var postList = new List<(Post?, User)>();
        
        foreach (var post in posts)
        {
            postList.Add((post, user));
        }

        return postList;
    }
    
    public async Task<(Post?,User)> Handle(GetPostById query)
    {
        
        var posts = await postRepository.FindPostById(query.Id);
        if (posts is null)
        {
            throw new Exception("ERROR Getting Post by ID");
        }
        
        
        var user = await externalUserAccountService.GetUserById(posts.UserId);
        if (user is null)
        {
            throw new Exception("ERROR Getting user by ID for post by ID");
        }

        return (posts, user);
    }
    
    public async Task<IEnumerable<(Post?, User)>?> Handle(GetAllPostsByCompanyId query)
    {
        var posts = await postRepository.GetAllPostsByCompanyId(query.CompanyId);
        if (posts is null)
        {
            throw new Exception("ERROR GETTING POSTS BY AUTHOR");
        }
        var postList = new List<(Post?, User)>();
        
        foreach (var post in posts)
        {
            var user = await externalUserAccountService.GetUserById(post.UserId);
            if (user is null)
            {
                 throw new Exception("ERROR GETTING USER BY ID FOR POSTS BY AUTHOR");
            }
            postList.Add((post, user));
        }

        return postList;
    }
    
    public async Task<IEnumerable<(Post?, User)>?> Handle(GetLikedPostsByUserid query)
    {
        var likedPosts = await likedPostRepository.GetLikedPostByUserIdAsync(query.UserId);
        if (likedPosts is null)
        {
            throw new Exception("ERROR GETTING LIKED POSTS BY USER ID");
        }
        var postList = new List<(Post?, User)>();
        
        foreach (var likedPost in likedPosts)
        {
            var post = await postRepository.FindPostById(likedPost.PostId);
            if (post is null)
            {
                throw new Exception("ERROR GETTING POST BY ID FOR LIKED POSTS BY USER ID");
            }
            var user = await externalUserAccountService.GetUserById(post.UserId);
            if (user is null)
            {
                throw new Exception("ERROR GETTING USER BY ID FOR LIKED POSTS BY USER ID");
            }
            postList.Add((post, user));
        }

        return postList;
    }
    
}