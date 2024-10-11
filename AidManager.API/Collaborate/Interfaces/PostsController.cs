using System.Net.Mime;
using AidManager.API.Authentication.Interfaces.REST.Transform;
using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.Collaborate.Domain.Model.Queries;
using AidManager.API.Collaborate.Domain.Services;
using AidManager.API.Collaborate.Interfaces.REST.Resources;
using AidManager.API.Collaborate.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AidManager.API.Collaborate.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class PostsController(IPostCommandService postCommandService, IPostQueryService postQueryService) : ControllerBase
{
    // create new post
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new post",
        Description = "Create a new post",
        OperationId = "CreatePost"
    )]
    [SwaggerResponse(201, "Post created", typeof(CreatePostResource))]
    public async Task<IActionResult> CreateNewPost([FromBody] CreatePostResource resource)
    {
        try
        {
            var command = CreatePostCommandFromResourceAssembler.ToCommandFromResource(resource);
            var post = await postCommandService.Handle(command);

            if (post == null) return BadRequest();

            var postResource = PostResourceFromEntityAssembler.ToResourceFromEntity(post);
            return Ok(postResource);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
        
    }
    
    
    //CreateAPost
     [HttpPost("{id}")]
        [SwaggerOperation(
            Summary = "Post comment",
            Description = "Add a comment to a post",
            OperationId = "PostComment"
        )]
        [SwaggerResponse(200, "New Comment Sent", typeof(CommentResource))]
        public async Task<IActionResult> AddPostComment([FromRoute] int id, [FromBody] AddCommentResource resource)
        {
            try
            {
                var command = CreateAddCommentCommandFromResourceAssembler.ToCommandFromResource(id, resource);
                var comments = await postCommandService.Handle(command);
    
                if (comments == null) return NotFound();
    
                var c = CommentResourceFromEntityAssembler.ToResourceFromEntity(comments);
                return Ok(c);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
            
        }    
        
     // update rating field of post by id
        [HttpPatch("{id}/rating")]
        [SwaggerOperation(
            Summary = "Update rating field of post by id",
            Description = "Update rating field of post by id",
            OperationId = "UpdatePostRating"
        )]
        [SwaggerResponse(200, "The post rating was updated: ", typeof(CreatePostResource))]
        public async Task<IActionResult> UpdatePostRating([FromRoute] int id)
        {
            try
            {
                var command = UpdatePostRatingCommandFromResourceAssembler.ToCommandFromResource(id);
                var post = await postCommandService.Handle(command);
    
                if (post == null) return NotFound();
    
                var postResource = PostResourceFromEntityAssembler.ToResourceFromEntity(post);
                return Ok(postResource);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
            
        }
        
    // obtain post by id
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get post by id",
        Description = "Get post by id",
        OperationId = "GetPostById"
    )]
    [SwaggerResponse(200, "The post was found", typeof(PostResource))]
    public async Task<IActionResult> GetPostById([FromRoute] int id)
    {
        try
        {
            var query = new GetPostById(id);
            var post = await postQueryService.Handle(query);

            if (post == null) return NotFound();

            var postResource = PostResourceFromEntityAssembler.ToResourceFromEntity(post);
            return Ok(postResource);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
        
    }
    
    // delete post by id
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete post by id",
        Description = "Delete post by id",
        OperationId = "DeletePostById"
    )]
    [SwaggerResponse(200, "The post was deleted", typeof(CreatePostResource))]
    public async Task<IActionResult> DeletePostById([FromRoute] int id)
    {
        try
        {
            var command = new DeletePostCommand(id);
            var post = await postCommandService.Handle(command);

            if (post == null) return NotFound();

            var postResource = PostResourceFromEntityAssembler.ToResourceFromEntity(post);
            return Ok(postResource);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
        
    }
    
    // get all posts by company id
    [HttpGet("company/{companyId}")]
    [SwaggerOperation(
        Summary = "Get all posts by company id",
        Description = "Get all posts by company id",
        OperationId = "GetAllPostsByCompanyId"
    )]
    [SwaggerResponse(200, "The posts by company id were found", typeof(CreatePostResource))]
    public async Task<IActionResult> GetAllPostsByCompanyId([FromRoute] int companyId)
    {
        try
        {
            var query = new GetAllPostsByCompanyId(companyId);
            var posts = await postQueryService.Handle(query);

            if (posts == null) return NotFound();

            var postResources = posts.Select(PostResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(postResources);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
        
    }
    
   
   
    
    [HttpGet("{id}/comments")]
    [SwaggerOperation(
        Summary = "Get comments",
        Description = "Get comments from a post",
        OperationId = "GetComments"
    )]
    public async Task<IActionResult> GetComments([FromRoute] int id)
    {
        var query = new GetCommentsByPostIdQuery(id);
        var comments = await postQueryService.Handle(query);
        if(comments == null) return BadRequest();
        var commentResources = comments.Select(CommentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(commentResources);
    }    
    
    [HttpDelete("{id}/comments/{commentId}")]
    [SwaggerOperation(
        Summary = "Delete comment",
        Description = "Delete a comment from a post",
        OperationId = "DeleteComment"
    )]
    public async Task<IActionResult> DeleteComment([FromRoute] int id, [FromRoute] int commentId)
    {
        var command = new DeleteCommentCommand(id, commentId);
        var deleted = await postCommandService.Handle(command);
        if (deleted == null)
            return Ok(new { status_code = 404, message = "Comment not found"});
        var c = CommentResourceFromEntityAssembler.ToResourceFromEntity(deleted);
        return Ok(new {status_code=202, message = "Comment deleted", data = c});
    }
}