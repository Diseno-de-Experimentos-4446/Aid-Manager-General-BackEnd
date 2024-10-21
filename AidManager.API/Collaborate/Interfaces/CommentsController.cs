using System.Net.Mime;
using AidManager.API.Collaborate.Domain.Model.Commands;
using AidManager.API.Collaborate.Domain.Model.Queries;
using AidManager.API.Collaborate.Domain.Services;
using AidManager.API.Collaborate.Interfaces.REST.Resources;
using AidManager.API.Collaborate.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AidManager.API.Collaborate.Interfaces;

[ApiController]
[Route("api/v1/posts/{postId}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CommentsController (IPostCommandService postCommandService, IPostQueryService postQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Post comment",
        Description = "Add a comment to a post",
        OperationId = "PostComment"
    )]
    [SwaggerResponse(200, "New Comment Sent", typeof(CommentResource))]
    public async Task<IActionResult> AddPostComment([FromRoute] int postId, [FromBody] AddCommentResource resource)
    {
        try
        {
            var command = CreateAddCommentCommandFromResourceAssembler.ToCommandFromResource(postId, resource);
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

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get comments",
        Description = "Get comments from a post",
        OperationId = "GetComments"
    )]
    public async Task<IActionResult> GetComments([FromRoute] int postId)
    {
        var query = new GetCommentsByPostIdQuery(postId);
        var comments = await postQueryService.Handle(query);
        if(comments == null) return BadRequest();
        var commentResources = comments.Select(CommentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(commentResources);
    }    
    
    [HttpDelete("{commentId}")]
    [SwaggerOperation(
        Summary = "Delete comment",
        Description = "Delete a comment from a post",
        OperationId = "DeleteComment"
    )]
    public async Task<IActionResult> DeleteComment([FromRoute] int postId, [FromRoute] int commentId)
    {
        var command = new DeleteCommentCommand(postId, commentId);
        var deleted = await postCommandService.Handle(command);
        if (deleted == null)
            return Ok(new { status_code = 404, message = "Comment not found"});
        var c = CommentResourceFromEntityAssembler.ToResourceFromEntity(deleted);
        return Ok(new {status_code=202, message = "Comment deleted", data = c});
    }
    
}