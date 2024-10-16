using AidManager.API.Authentication.Domain.Model.Entities;
using AidManager.API.ManageTasks.Domain.Model.Aggregates;
using AidManager.API.ManageTasks.Interfaces.REST.Resources;

namespace AidManager.API.ManageTasks.Interfaces.REST.Transform;

public static class ProjectResourceFromEntityAssembler
{
    public static ProjectResource ToResourceFromEntity(Project entity) =>
        new ProjectResource(entity.Id,entity.Name, entity.Description,entity.ProjectDate, entity.ProjectTime, entity.ProjectLocation, entity.CompanyId,entity.TeamMembers?.ToList() ?? new List<User>(),entity.ImageUrl?.Select(img => img.Url).ToList() ?? new List<string>());
}