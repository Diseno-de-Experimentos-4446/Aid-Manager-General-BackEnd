using System.Net.Mime;
using AidManager.API.ManageCosts.Domain.Model.Queries;
using AidManager.API.ManageCosts.Domain.Services;
using AidManager.API.ManageCosts.Interfaces.REST.Resources;
using AidManager.API.ManageCosts.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AidManager.API.ManageCosts.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AnalyticsController(
    IAnalyticsCommandService analyticsCommandService,
    IAnalyticsQueryService analyticsQueryService
    ) : ControllerBase
{
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Analytics Data",
        Description = "Add new Data to the Analytics",
        OperationId = "CreateData"
    )]
    [SwaggerResponse(201, "Analytics created", typeof(CreateAnalyticsResource))]
    public async Task<IActionResult> CreateNewAnalytics([FromBody] CreateAnalyticsResource resource)
    {
        var command = CreateAnalyticsCommandFromResourceAssembler.ToCommandFromResource(resource);
        var analytic = await analyticsCommandService.Handle(command);
        
        if (analytic == null)
        {
            return NotFound();
        }
        
        var analyticResource = AnalyticsResourceFromEntityAssembler.ToResourceFromEntity(analytic);
        return Ok(analyticResource);
    }
    
    [HttpGet("{projectId}")]
    [SwaggerOperation(
        Summary = "Get Analytics by Project Id",
        Description = "gets Analytics by Project Id",
        OperationId = "GetAnalytics"
    )]
    [SwaggerResponse(200, "Analytics found", typeof(AnalyticsResource))]
    public async Task<IActionResult> GetAnalyticsByProjectId([FromRoute] int projectId)
    {
        var query = new GetAnalyticsByProjectId(projectId);
        var analytic = await analyticsQueryService.Handle(query);
        
        if (analytic == null)
        {
            return NotFound();
        }
        
        var analyticResource = AnalyticsResourceFromEntityAssembler.ToResourceFromEntity(analytic);
        return Ok(analyticResource);
    }
    
    [HttpPatch("BarData")]
    [SwaggerOperation(
        Summary = "Update BarData",
        Description = "Update the BarData list",
        OperationId = "UpdateBarData"
    )]
    [SwaggerResponse(200, "Analytics updated", typeof(UpdateLinesChartBarResource))]
    public async Task<IActionResult> UpdateAnalyticPayments([FromBody] UpdateLinesChartBarResource resource)
    {
        var command = UpdateLinesChartBarCommandFromResourceAssembler.ToCommandFromResource(resource.Id, resource);
        var analytic = await analyticsCommandService.Handle(command);
        
        if (analytic == null)
        {
            return NotFound();
        }
        
        var analyticResource = AnalyticsResourceFromEntityAssembler.ToResourceFromEntity(analytic);
        return Ok(analyticResource);
    }
    
    [HttpPatch("lines")]
    [SwaggerOperation(
        Summary = "Update LineBarData",
        Description = "Update the LineBarData list",
        OperationId = "UpdateLineBarData"
    )]
    [SwaggerResponse(200, "Analytics updated", typeof(UpdateBarDataResource))]
    public async Task<IActionResult> UpdateAnalyticLines([FromBody] UpdateBarDataResource resource)
    {
        var command = UpdateBarDataCommandFromResourceAssembler.ToCommandFromResource(resource.Id, resource);
        var analytic = await analyticsCommandService.Handle(command);
        
        if (analytic == null)
        {
            return NotFound();
        }
        
        var analyticResource = AnalyticsResourceFromEntityAssembler.ToResourceFromEntity(analytic);
        return Ok(analyticResource);
    }
    
    [HttpPatch("tasks")]
    [SwaggerResponse(200, "Analytics updated", typeof(UpdateAnalyticTasksResource))]
    public async Task<IActionResult> UpdateAnalyticTasks([FromBody] UpdateAnalyticTasksResource resource)
    {
        var command = UpdateAnalyticTasksCommandFromResourceAssembler.ToCommandFromResource(resource.Id, resource);
        var analytic = await analyticsCommandService.Handle(command);
        
        if (analytic == null)
        {
            return NotFound();
        }
        
        var analyticResource = AnalyticsResourceFromEntityAssembler.ToResourceFromEntity(analytic);
        return Ok(analyticResource);
    }
    
    [HttpPatch("progressbar")]
    [SwaggerResponse(200, "Analytics updated", typeof(UpdateAnalyticProgressbarResource))]
    public async Task<IActionResult> UpdateAnalyticProgressbar([FromBody] UpdateAnalyticProgressbarResource resource)
    {
        var command = UpdateAnalyticProgressbarCommandFromResourceAssembler.ToCommandFromResource(resource.Id, resource);
        var analytic = await analyticsCommandService.Handle(command);
        
        if (analytic == null)
        {
            return NotFound();
        }
        
        var analyticResource = AnalyticsResourceFromEntityAssembler.ToResourceFromEntity(analytic);
        return Ok(analyticResource);
    }
    
    [HttpPatch("status")]
    [SwaggerResponse(200, "Analytics updated", typeof(UpdateAnalyticStatusResource))]
    public async Task<IActionResult> UpdateAnalyticStatus([FromBody] UpdateAnalyticStatusResource resource)
    {
        var command = UpdateAnalyticStatusCommandFromResourceAssembler.ToCommandFromResource(resource.Id, resource);
        var analytic = await analyticsCommandService.Handle(command);
        
        if (analytic == null)
        {
            return NotFound();
        }
        
        var analyticResource = AnalyticsResourceFromEntityAssembler.ToResourceFromEntity(analytic);
        return Ok(analyticResource);
    }
    
}